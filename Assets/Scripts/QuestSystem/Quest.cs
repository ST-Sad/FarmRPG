using System.Collections.Generic;
using UnityEngine;
public enum QuestObjectiveType
{
    Collect, // 收集物品
    Kill,    // 击杀敌人
    Talk,    // 与NPC对话
    Reach,   // 到达指定地点
    Use      // 使用物品
}

// 任务目标数据
[System.Serializable]
public class QuestObjectiveData
{
    public QuestObjectiveType type;
    public string targetID;      // 物品Name、敌人Name或NPC Name
    public int requiredAmount;
    [TextArea] public string description;
}

// 任务奖励
[System.Serializable]
public class Reward
{
    public ItemData item;        // 奖励物品
    public int amount = 1;       // 数量
    public int experience = 0;   // 经验奖励
}

// 任务配置数据 (ScriptableObject)
[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest Data")]
public class QuestData : ScriptableObject
{
    public string questID;       // 唯一标识符
    public string title;         // 任务标题
    [TextArea] public string description; // 任务描述
    public Sprite icon;          // 任务图标
    public List<QuestObjectiveData> objectives = new List<QuestObjectiveData>();
    public List<Reward> rewards = new List<Reward>();
    
    [Header("依赖任务")]
    public List<QuestData> prerequisiteQuests; // 需要先完成的任务
}

// 运行时任务实例
public class Quest
{
    public QuestData data { get; private set; }
    public Dictionary<string, int> currentProgress { get; private set; }
    public bool isCompleted;
    
    public Quest(QuestData data)
    {
        this.data = data;
        this.currentProgress = new Dictionary<string, int>();
        this.isCompleted = false;
        
        // 初始化所有目标进度为0
        foreach (var objective in data.objectives)
        {
            string objectiveKey = GetObjectiveKey(objective);
            currentProgress[objectiveKey] = 0;
        }
    }
    
    // 更新任务进度
    public void IncrementProgress(string objectiveKey, int amount)
    {
        if (currentProgress.ContainsKey(objectiveKey))
        {
            currentProgress[objectiveKey] += amount;
        }
    }
    
    // 检查任务是否完成
    public bool IsComplete()
    {
        if (isCompleted) return true;
        
        foreach (var objective in data.objectives)
        {
            string objectiveKey = GetObjectiveKey(objective);
            if (currentProgress[objectiveKey] < objective.requiredAmount)
            {
                return false;
            }
        }
        return true;
    }
    
    // 生成目标唯一键
    public static string GetObjectiveKey(QuestObjectiveData objective)
    {
        return $"{objective.type}_{objective.targetID}";
    }
}
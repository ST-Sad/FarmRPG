using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    [Header("任务数据库")]
    [SerializeField] private List<QuestData> questDatabase = new List<QuestData>();
    [Header("事件")]
    public UnityEvent<Quest> OnQuestAccepted = new UnityEvent<Quest>();
    public UnityEvent<Quest> OnQuestProgressUpdated = new UnityEvent<Quest>();
    public UnityEvent<Quest> OnQuestCompleted = new UnityEvent<Quest>();
    public InventoryManager InventoryManager;
    public CharacterStats CharacterStats;
    private Dictionary<string, Quest> activeQuests = new Dictionary<string, Quest>();
    private Dictionary<string, Quest> completedQuests = new Dictionary<string, Quest>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public bool AcceptQuest(QuestData questData)
    {
        // 检查是否已接受或已完成
        if (activeQuests.ContainsKey(questData.questID) ||
            completedQuests.ContainsKey(questData.questID))
        {
            Debug.LogWarning($"任务 {questData.title} 已存在");
            return false;
        }

        // 检查前置任务
        foreach (var prereq in questData.prerequisiteQuests)
        {
            if (!completedQuests.ContainsKey(prereq.questID))
            {
                Debug.LogWarning($"需要先完成前置任务: {prereq.title}");
                return false;
            }
        }
        // 创建新任务
        var quest = new Quest(questData);
        activeQuests.Add(questData.questID, quest);
        OnQuestAccepted.Invoke(quest);
        Debug.Log($"接受任务: {questData.title}");
        return true;
    }
    public void UpdateQuestProgress(QuestObjectiveType type, string targetID, int amount = 1)
    {
        string objectiveKey = $"{type}_{targetID}";
        bool anyProgressUpdated = false;
        foreach (var quest in activeQuests)
        {
            Quest q = quest.Value;
            if (q.currentProgress.ContainsKey(objectiveKey))
            {
                CompleteQuest(q);
            }
            else
            {
                OnQuestProgressUpdated.Invoke(q);
            }
        }
        if (!anyProgressUpdated)
        {
            Debug.LogWarning($"没有任务需要 {type}:{targetID}");
        }
    }
    private void CompleteQuest(Quest quest)
    {
        quest.isCompleted = true;
        activeQuests.Remove(quest.data.questID);
        completedQuests.Add(quest.data.questID, quest);
        GiveAwards(quest);
        OnQuestCompleted.Invoke(quest);
        Debug.Log($"完成任务: {quest.data.title}");
    }
    private void GiveAwards(Quest quest)
    {
        foreach (var reward in quest.data.rewards)
        {
            if (reward.item != null)
            {
                InventoryManager.AddItem(reward.item, reward.amount);
            }
            if (reward.experience > 0)
            {
                CharacterStats.AddExp(reward.experience);
                Debug.Log($"获得经验: {reward.experience}");
            }
        }
    }
    public QuestStatus GetQuestStatus(string questID)
    {
        if (completedQuests.ContainsKey(questID))
        {
            return QuestStatus.Completed;
        }
        if (activeQuests.ContainsKey(questID))
        {
            return QuestStatus.InProgress;
        }
        return QuestStatus.NotAccepted;
    }
    // 获取所有进行中的任务
    public List<Quest> GetActiveQuests()
    {
        return new List<Quest>(activeQuests.Values);
    }
    
    // 获取所有已完成的任务
    public List<Quest> GetCompletedQuests()
    {
        return new List<Quest>(completedQuests.Values);
    }
}
public enum QuestStatus
{
    NotAccepted, // 未接受
    InProgress,   // 进行中
    Completed     // 已完成
}

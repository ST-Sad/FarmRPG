using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestLogUI : abstractUIPanel
{
    [Header("UI References")]
    [SerializeField] private Transform activeQuestsContent;
    [SerializeField] private Transform completedQuestsContent;
    [SerializeField] private GameObject questEntryPrefab;
    [SerializeField] private Text questTitleText;
    [SerializeField] private Text questDescriptionText;
    [SerializeField] private Transform objectivesContainer;
    [SerializeField] private GameObject objectiveEntryPrefab;
    
    private Quest selectedQuest;

    protected override void Awake()
{
    base.Awake(); // 确保基类初始化
    
    // 延迟初始化，确保QuestManager已准备好
    StartCoroutine(InitializeAfterFrame());
}

private IEnumerator InitializeAfterFrame()
{
    yield return null; // 等待一帧
    
    if (QuestManager.Instance == null)
    {
        Debug.LogError("QuestManager instance not found!");
        yield break;
    }

    QuestManager.Instance.OnQuestAccepted.AddListener(RefreshUI);
    QuestManager.Instance.OnQuestProgressUpdated.AddListener(RefreshUI);
    QuestManager.Instance.OnQuestCompleted.AddListener(RefreshUI);
    
    // 初始刷新
    RefreshUI();
}

    public override void Open()
    {
        base.Open();
        RefreshUI();
    }
    public override void Refresh()
    {
        RefreshUI();
    }
    private void RefreshUI(Quest quest = null)
    {
        ClearQuestList();
        ClearObjectives();

        // 显示进行中任务
        foreach (var q in QuestManager.Instance.GetActiveQuests())
        {
            CreateQuestEntry(q, activeQuestsContent);
        }

        // 显示已完成任务
        foreach (var q in QuestManager.Instance.GetCompletedQuests())
        {
            CreateQuestEntry(q, completedQuestsContent);
        }

        // 保持当前选择或选择第一个任务
        if (quest != null)
        {
            SelectQuest(quest);
        }
        else if (selectedQuest == null && QuestManager.Instance.GetActiveQuests().Count > 0)
        {
            SelectQuest(QuestManager.Instance.GetActiveQuests()[0]);
        }
    }

    private void CreateQuestEntry(Quest quest, Transform parent)
    {
        GameObject entry = Instantiate(questEntryPrefab, parent);
        entry.GetComponentInChildren<Text>().text = quest.data.title;
        entry.GetComponent<Button>().onClick.AddListener(() => SelectQuest(quest));
    }

    private void SelectQuest(Quest quest)
    {
        selectedQuest = quest;
        questTitleText.text = quest.data.title;
        questDescriptionText.text = quest.data.description;
        
        // 显示任务目标
        foreach (var objective in quest.data.objectives)
        {
            string objectiveKey = Quest.GetObjectiveKey(objective);
            int currentAmount = quest.currentProgress[objectiveKey];
            
            GameObject objectiveEntry = Instantiate(objectiveEntryPrefab, objectivesContainer);
            Text text = objectiveEntry.GetComponentInChildren<Text>();
            text.text = $"- {objective.description} ({currentAmount}/{objective.requiredAmount})";
            
            // 已完成的目标用颜色区分
            if (currentAmount >= objective.requiredAmount)
            {
                text.color = Color.green;
            }
        }
    }

    private void ClearQuestList()
    {
        foreach (Transform child in activeQuestsContent)
        {
            Destroy(child.gameObject);
        }
        
        foreach (Transform child in completedQuestsContent)
        {
            Destroy(child.gameObject);
        }
    }

    private void ClearObjectives()
    {
        foreach (Transform child in objectivesContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
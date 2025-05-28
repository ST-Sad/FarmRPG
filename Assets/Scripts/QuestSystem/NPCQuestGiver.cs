using UnityEngine;


public class NPCQuestGiver : MonoBehaviour
{
    [Header("任务设置")]
    public QuestData questToGive;
    public bool completesQuest; // 是否也是任务完成NPC
    
    [Header("对话设置")]
    public DialogueData beforeQuestDialogue;
    public DialogueData duringQuestDialogue;
    public DialogueData afterQuestDialogue;
    
    public void Interact()
    {
        QuestStatus status = QuestManager.Instance.GetQuestStatus(questToGive.questID);

        // 根据任务状态选择对话
        DialogueData dialogueToUse = null;

        switch (status)
        {
            case QuestStatus.NotAccepted:
                dialogueToUse = beforeQuestDialogue;
                break;
            case QuestStatus.InProgress:
                dialogueToUse = duringQuestDialogue;
                break;
            case QuestStatus.Completed:
                dialogueToUse = afterQuestDialogue;
                break;
        }

        // 显示对话
        if (dialogueToUse != null)
        {
            DialogueManager.Instance.StartDialogue(dialogueToUse);
            HandleQuestAfterDialogue(status);
        }
        else
        {
            HandleQuestAfterDialogue(status);
        }
    }
    
    private void HandleQuestAfterDialogue(QuestStatus status)
    {
        if (status == QuestStatus.NotAccepted)
        {
            // 尝试接受任务
            if (QuestManager.Instance.AcceptQuest(questToGive))
            {
                // 任务接受成功
            }
        }
        else if (status == QuestStatus.InProgress && completesQuest)
        {
            // 检查任务是否完成
            Quest quest = QuestManager.Instance.GetActiveQuests()
                .Find(q => q.data.questID == questToGive.questID);
                
            if (quest != null && quest.IsComplete())
            {
                // 标记任务完成
                QuestManager.Instance.UpdateQuestProgress(
                    QuestObjectiveType.Talk, 
                    gameObject.name, 
                    1
                );
            }
        }
    }
}
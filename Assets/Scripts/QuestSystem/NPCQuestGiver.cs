using System.Collections.Generic;
using UnityEngine;


public class NPCQuestGiver : MonoBehaviour
{
    [Header("任务设置")]
    public List<QuestData> questToGive;
    public bool completesQuest; // 是否也是任务完成NPC
    
    [Header("对话设置")]
    public List<DialogueData> beforeQuestDialogue;
    public List<DialogueData> duringQuestDialogue;
    public List<DialogueData> afterQuestDialogue;
    public KeyCode interactionKey = KeyCode.E;
    private  int n=0;

    private GameObject player;
    private bool playerInRange = false;
        private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;
        }
    }

    public void Interact()
    {
        QuestStatus status = QuestManager.Instance.GetQuestStatus(questToGive[n].questID);

        // 根据任务状态选择对话
        DialogueData dialogueToUse = null;

        switch (status)
        {
            case QuestStatus.NotAccepted:
                dialogueToUse = beforeQuestDialogue[n];
                break;
            case QuestStatus.InProgress:
                dialogueToUse = duringQuestDialogue[n];
                break;
            case QuestStatus.Completed:
                dialogueToUse = afterQuestDialogue[n];
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
            if (QuestManager.Instance.AcceptQuest(questToGive[n]))
            {
                Debug.Log("Task successfully accepted!");

                // 任务接受成功
            }

            else Debug.LogWarning("Failed to accept task!");
        }
        else if (status == QuestStatus.InProgress && completesQuest)
        {
            // 检查任务是否完成
            Quest quest = QuestManager.Instance.GetActiveQuests()
                .Find(q => q.data.questID == questToGive[n].questID);

            if (quest != null && quest.IsComplete())
            {
                // 标记任务完成
                QuestManager.Instance.UpdateQuestProgress(
                    QuestObjectiveType.Talk,
                    gameObject.name,
                    1
                );
                n++;
            }
        }
    }
}
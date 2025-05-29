using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NPCInteractionHandler : MonoBehaviour
{
    private NPCDialogue dialogue;
    private NPCQuestGiver questGiver;
    private bool playerInRange = false;

    private void Awake()
    {
        dialogue = GetComponent<NPCDialogue>();
        questGiver = GetComponent<NPCQuestGiver>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (dialogue != null && questGiver != null && playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.T)) // 使用 T 键接受任务
            {
                if (!DialogueManager.Instance.IsInDialogue())
                {
                    questGiver.Interact();
                }
            }
        }
    }
}
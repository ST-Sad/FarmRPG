using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public DialogueData dialogueData;
    public bool autoTrigger = false;
    public KeyCode interactionKey = KeyCode.E;
    public float interactionRange = 2f;

    private bool playerInRange = false;
    private GameObject player;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            TriggerDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;
            
            if (autoTrigger)
            {
                TriggerDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
        }
    }

    public void TriggerDialogue()
    {
        if (player == null || DialogueManager.Instance.IsInDialogue()) return;
        
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            DialogueManager.Instance.StartDialogue(dialogueData);
        }
    }

    // 在场景中显示交互范围(仅编辑器)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
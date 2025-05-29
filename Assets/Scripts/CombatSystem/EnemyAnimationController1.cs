using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;
    private EnemyAI enemyAI;
    private Transform player;
    private Vector2 lastMoveDirection = Vector2.down;

    [SerializeField] private float sleepTriggerDistance = 8f;
    [SerializeField] private float idleTriggerDistance = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
        player = GameObject.FindWithTag("Player")?.transform;

        if (animator == null) Debug.LogError("Animator not found on " + gameObject.name);
        if (enemyAI == null) Debug.LogError("EnemyAI not found on " + gameObject.name);
        if (player == null) Debug.LogError("Player not found in scene");
    }

    void Update()
    {
        if (animator == null || enemyAI == null || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        Vector2 moveDirection = Vector2.zero;

        if (distance <= enemyAI.chaseRange)
        {
            moveDirection = (player.position - transform.position).normalized;
        }
        else
        {
            moveDirection = (enemyAI.PatrolTarget - (Vector2)transform.position).normalized; // Ê¹ÓÃÊôÐÔ
        }

        if (moveDirection.magnitude > 0.1f)
        {
            lastMoveDirection = moveDirection;
        }

        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetFloat("Speed", moveDirection.magnitude);

        if (distance > sleepTriggerDistance)
        {
            animator.SetBool("IsSleeping", true);
        }
        else if (distance > idleTriggerDistance)
        {
            animator.SetBool("IsSleeping", false);
        }
        else
        {
            animator.SetBool("IsSleeping", false);
        }
    }
}
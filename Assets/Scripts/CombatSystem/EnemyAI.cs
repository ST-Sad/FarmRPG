// EnemyAI.cs
using UnityEngine;

/// <summary>
/// �򵥵ĵ��� AI��Ѳ�ߺ�׷����ң����ڿ���ʱ����˺�
/// </summary>
public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;      // �ƶ��ٶ�
    public float chaseRange = 5f;     // ���ֲ�׷����ҵķ�Χ
    public float attackRange = 0.5f;  // ��������
    public int attackDamage = 10;     // �����˺�
    public float attackCooldown = 1.5f; // ������ȴʱ��
    public float patrolRadius = 3f;   // Ѳ�߰뾶������ʼ��Ϊ���ģ�
    private float lastAttackTime = 0f;
    private Transform player;
    private Vector2 startPos;
    private Vector2 patrolTarget;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        startPos = transform.position;
        ChooseNewPatrolTarget();
    }

    void Update()
    {
        if (player == null) return;
        float distance = Vector2.Distance(transform.position, player.position);
        // ׷���߼�
        if (distance <= chaseRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            // �������
            if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                CharacterStats playerStats = player.GetComponent<CharacterStats>();
                if (playerStats != null)
                {
                    playerStats.TakeDamage(attackDamage);
                }
                lastAttackTime = Time.time;
            }
        }
        else
        {
            // Ѳ���߼�����Ŀ����ƶ��������ѡ����Ŀ��
            if (Vector2.Distance(transform.position, patrolTarget) < 0.1f)
            {
                ChooseNewPatrolTarget();
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolTarget, moveSpeed * Time.deltaTime);
            }
        }
    }

    // ���ѡ���µ�Ѳ��Ŀ���
    private void ChooseNewPatrolTarget()
    {
        Vector2 randomOffset = Random.insideUnitCircle * patrolRadius;
        patrolTarget = startPos + randomOffset;
    }
}

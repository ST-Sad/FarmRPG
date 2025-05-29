// EnemyAI.cs
using UnityEngine;

/// <summary>
/// 简单的敌人 AI：巡逻和追击玩家，并在靠近时造成伤害
/// </summary>
public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;      // 移动速度
    public float chaseRange = 5f;     // 发现并追击玩家的范围
    public float attackRange = 0.5f;  // 攻击距离
    public int attackDamage = 10;     // 攻击伤害
    public float attackCooldown = 1.5f; // 攻击冷却时间
    public float patrolRadius = 3f;   // 巡逻半径（以起始点为中心）
    private float lastAttackTime = 0f;
    private Transform player;
    private Vector2 startPos;
    private Vector2 patrolTarget;

    // 添加一个公共 getter 属性
    public Vector2 PatrolTarget => patrolTarget;

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
        // 追击逻辑
        if (distance <= chaseRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            // 攻击玩家
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
            // 巡逻逻辑：向目标点移动，到达后选择新目标
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

    // 随机选择新的巡逻目标点
    private void ChooseNewPatrolTarget()
    {
        Vector2 randomOffset = Random.insideUnitCircle * patrolRadius;
        patrolTarget = startPos + randomOffset;
    }
}

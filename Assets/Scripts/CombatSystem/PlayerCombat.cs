// PlayerCombat.cs
using System.Collections;
using UnityEngine;

// ���ս������
public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;     // �����ж���
    public float attackRange = 0.5f;  // ������Χ
    public LayerMask enemyLayers;     // �������ڲ�
    public int attackDamage = 20;     // ��ͨ�����˺�
    public float attackRate = 2f;     // ����Ƶ��
    private float nextAttackTime = 0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ʹ�ÿո�����й������������ȴʱ��
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void Attack()
    {
        // ���Ź�������
        if (animator != null)
        {
            animator.SetTrigger("Attack"); // ������������:contentReference[oaicite:7]{index=7}
        }
        // ��⹥����Χ�ڵĵ���
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            EntityStats stats = enemy.GetComponent<EntityStats>();
            if (stats != null)
            {
                stats.TakeDamage(attackDamage);
            }
        }
    }

    // ��Scene��ͼ�л��ƹ�����Χ
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

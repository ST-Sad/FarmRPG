// PlayerCombat.cs
using UnityEngine;
using System.Collections;

/// <summary>
/// ���ս�����ƣ�������ͨ�����ͼ���ʩ��
/// </summary>
public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;    // �����ж����ĵ�
    public float attackRange = 0.5f; // ��ͨ������Χ
    public LayerMask enemyLayers;    // �������ڲ㼶
    public int attackDamage = 20;    // ��ͨ�����˺�
    public float attackRate = 2f;    // ����Ƶ�ʣ���/�룩
    public SkillData skillData;      // ��������
    private float nextAttackTime = 0f;
    private float nextSkillTime = 0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ��ͨ�������ո������
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
        // �����ͷţ��� Shift ������
        if (skillData != null && Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= nextSkillTime)
        {
            CastSkill();
            nextSkillTime = Time.time + skillData.cooldown;
        }
    }

    /// <summary>
    /// ִ����ͨ��������ⷶΧ�ڵ��˲�����˺�
    /// </summary>
    void Attack()
    {
        if (animator != null) animator.SetTrigger("Attack");
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

    /// <summary>
    /// ʩ�ż��ܣ����ż��ܶ������Է�Χ�ڵ�����ɼ����˺�
    /// </summary>
    void CastSkill()
    {
        if (animator != null) animator.SetTrigger("Skill");
        // ʾ�����������÷�ΧΪ��ͨ����������
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange * 2, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            EntityStats stats = enemy.GetComponent<EntityStats>();
            if (stats != null)
            {
                stats.TakeDamage(skillData.damage);
            }
        }
    }

    // �ڱ༭���п��ӻ�������Χ
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

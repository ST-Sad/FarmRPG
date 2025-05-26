// PlayerCombat.cs
using UnityEngine;
using System.Collections;

/// <summary>
/// 玩家战斗控制：处理普通攻击和技能施放
/// </summary>
public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;    // 攻击判定中心点
    public float attackRange = 0.5f; // 普通攻击范围
    public LayerMask enemyLayers;    // 敌人所在层级
    public int attackDamage = 20;    // 普通攻击伤害
    public float attackRate = 2f;    // 攻击频率（次/秒）
    public SkillData skillData;      // 技能配置
    private float nextAttackTime = 0f;
    private float nextSkillTime = 0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 普通攻击：空格键触发
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
        // 技能释放：左 Shift 键触发
        if (skillData != null && Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= nextSkillTime)
        {
            CastSkill();
            nextSkillTime = Time.time + skillData.cooldown;
        }
    }

    /// <summary>
    /// 执行普通攻击，检测范围内敌人并造成伤害
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
    /// 施放技能，播放技能动画并对范围内敌人造成技能伤害
    /// </summary>
    void CastSkill()
    {
        if (animator != null) animator.SetTrigger("Skill");
        // 示例：技能作用范围为普通攻击的两倍
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

    // 在编辑器中可视化攻击范围
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

// EntityStats.cs
using UnityEngine;

/// <summary>
/// 管理实体的生命值和死亡逻辑
/// </summary>
public class EntityStats : MonoBehaviour
{
    public int maxHealth = 100;    // 最大生命值
    public int currentHealth;      // 当前生命值
    public int expValue = 1;       // 死亡时给予的经验值
    public string enemyName;

    void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// 实体受到伤害
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    /// <summary>
    /// 实体死亡处理：销毁对象并给予玩家经验
    /// </summary>
    private void Die()
    {
        // 给玩家角色添加经验
        CharacterStats playerStats = FindObjectOfType<CharacterStats>();
        if (playerStats != null)
        {
            playerStats.AddExp(expValue);
        }
        QuestManager.Instance.UpdateQuestProgress(
            QuestObjectiveType.Kill, 
            enemyName, 
            1
        );
        Destroy(gameObject);
    }
}

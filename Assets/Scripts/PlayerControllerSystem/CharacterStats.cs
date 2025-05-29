// CharacterStats.cs
using UnityEngine;

/// <summary>
/// 管理角色实时属性（当前生命、体力、经验、等级等）和伤害处理
/// </summary>
public class CharacterStats : MonoBehaviour
{
    public CharacterData baseData; // 基础属性数据
    [HideInInspector] public int maxHealth;
    [HideInInspector] public int currentHealth;
    [HideInInspector] public int attack;
    [HideInInspector] public int defense;
    [HideInInspector] public int currentStamina;
    [HideInInspector] public int level = 1;
    [HideInInspector] public int currentExp = 0;
    [HideInInspector] public int expToNextLevel = 100;

    //攻击状态检测
    public event System.Action OnTakeDamage;
   


    void Start()
    {
        // 应用基础数据初始化属性
        maxHealth = baseData.maxHealth;
        currentHealth = maxHealth;
        attack = baseData.attack;
        defense = baseData.defense;
        currentStamina = baseData.maxStamina;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // 体力自然恢复
        if (currentStamina < baseData.maxStamina)
        {
            currentStamina = Mathf.Min(baseData.maxStamina, currentStamina + (int)(baseData.staminaRegen * Time.deltaTime));
        }
    }

    /// <summary>
    /// 受到伤害：扣除防御值后的实际伤害
    /// </summary>
    public void TakeDamage(int damage)
    {
        int actual = Mathf.Max(damage - defense, 0);
        currentHealth -= actual;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        OnTakeDamage?.Invoke();
    }

    /// <summary>
    /// 增加经验并检测升级阈值，升级时提升属性
    /// </summary>
    public void AddExp(int exp)
    {
        currentExp += exp;
        while (currentExp >= expToNextLevel)
        {
            currentExp -= expToNextLevel;
            level++;
            // 提高升级阈值和属性增长
            expToNextLevel = (int)(expToNextLevel * 1.5f);
            maxHealth += 10;
            attack += 2;
            defense += 1;
            currentHealth = maxHealth; // 升级后恢复生命值
        }
    }

    /// <summary>
    /// 角色死亡处理
    /// </summary>
    private void Die()
    {
        // 可播放死亡动画等，这里简单销毁对象
        Destroy(gameObject);
    }
}

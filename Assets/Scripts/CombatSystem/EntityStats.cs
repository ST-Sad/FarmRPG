// EntityStats.cs
using UnityEngine;

/// <summary>
/// ����ʵ�������ֵ�������߼�
/// </summary>
public class EntityStats : MonoBehaviour
{
    public int maxHealth = 100;    // �������ֵ
    public int currentHealth;      // ��ǰ����ֵ
    public int expValue = 1;       // ����ʱ����ľ���ֵ
    public string enemyName;

    void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// ʵ���ܵ��˺�
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
    /// ʵ�������������ٶ��󲢸�����Ҿ���
    /// </summary>
    private void Die()
    {
        // ����ҽ�ɫ��Ӿ���
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

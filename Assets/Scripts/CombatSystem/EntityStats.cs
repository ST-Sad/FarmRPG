// EntityStats.cs
using UnityEngine;

// ����ʵ������Ժ�����ֵ
public class EntityStats : MonoBehaviour
{
    public int maxHealth = 100;    // �������ֵ
    public int currentHealth;      // ��ǰ����ֵ

    void Start()
    {
        currentHealth = maxHealth;
    }

    // �ܵ��˺�����
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    // ʵ������
    private void Die()
    {
        // ��������߼����粥�Ŷ���
        Destroy(gameObject);
    }
}

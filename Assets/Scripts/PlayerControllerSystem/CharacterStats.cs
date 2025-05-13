// CharacterStats.cs
using UnityEngine;

// �����ɫ���Ժ��˺�����
public class CharacterStats : MonoBehaviour
{
    public CharacterData baseData; // ��ɫ�������ݣ�ScriptableObject��
    public int currentHealth;
    public int attack;
    public int defense;

    void Start()
    {
        // ��ʼ������
        currentHealth = baseData.maxHealth;
        attack = baseData.attack;
        defense = baseData.defense;
    }

    // �ܵ��˺����۳��������������ֵ
    public void TakeDamage(int damage)
    {
        int actualDamage = Mathf.Max(damage - defense, 0);
        currentHealth -= actualDamage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    // ��ɫ��������
    private void Die()
    {
        // ��������ٶ���Ҳ������������������߼�
        Destroy(gameObject);
    }
}

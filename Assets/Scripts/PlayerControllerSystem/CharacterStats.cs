// CharacterStats.cs
using UnityEngine;

/// <summary>
/// �����ɫʵʱ���ԣ���ǰ���������������顢�ȼ��ȣ����˺�����
/// </summary>
public class CharacterStats : MonoBehaviour
{
    public CharacterData baseData; // ������������
    [HideInInspector] public int maxHealth;
    [HideInInspector] public int currentHealth;
    [HideInInspector] public int attack;
    [HideInInspector] public int defense;
    [HideInInspector] public int currentStamina;
    [HideInInspector] public int level = 1;
    [HideInInspector] public int currentExp = 0;
    [HideInInspector] public int expToNextLevel = 100;

    //����״̬���
    public event System.Action OnTakeDamage;
   


    void Start()
    {
        // Ӧ�û������ݳ�ʼ������
        maxHealth = baseData.maxHealth;
        currentHealth = maxHealth;
        attack = baseData.attack;
        defense = baseData.defense;
        currentStamina = baseData.maxStamina;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // ������Ȼ�ָ�
        if (currentStamina < baseData.maxStamina)
        {
            currentStamina = Mathf.Min(baseData.maxStamina, currentStamina + (int)(baseData.staminaRegen * Time.deltaTime));
        }
    }

    /// <summary>
    /// �ܵ��˺����۳�����ֵ���ʵ���˺�
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
    /// ���Ӿ��鲢���������ֵ������ʱ��������
    /// </summary>
    public void AddExp(int exp)
    {
        currentExp += exp;
        while (currentExp >= expToNextLevel)
        {
            currentExp -= expToNextLevel;
            level++;
            // ���������ֵ����������
            expToNextLevel = (int)(expToNextLevel * 1.5f);
            maxHealth += 10;
            attack += 2;
            defense += 1;
            currentHealth = maxHealth; // ������ָ�����ֵ
        }
    }

    /// <summary>
    /// ��ɫ��������
    /// </summary>
    private void Die()
    {
        // �ɲ������������ȣ���������ٶ���
        Destroy(gameObject);
    }
}

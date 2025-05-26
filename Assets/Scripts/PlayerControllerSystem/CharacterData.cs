// CharacterData.cs
using UnityEngine;

/// <summary>
/// ��ɫ�����������ݣ��� ScriptableObject ���壨������������������ٶȡ������ȣ�
/// </summary>
[CreateAssetMenu(fileName = "NewCharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public int maxHealth = 100;     // �������ֵ
    public int attack = 10;         // ������
    public int defense = 5;         // ������
    public float moveSpeed = 5f;    // �ƶ��ٶ�
    public int maxStamina = 100;    // �������
    public float staminaRegen = 5f; // �����ָ��ٶ�
    public float staminaCostPerSecond = 5f; // �ƶ�ʱÿ�����ĵ�����
}

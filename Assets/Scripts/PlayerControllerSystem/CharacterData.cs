// CharacterData.cs
using UnityEngine;

// �����ɫ�������Ե� ScriptableObject
[CreateAssetMenu(fileName = "NewCharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public int maxHealth = 100; // �������ֵ
    public int attack = 10;     // ������
    public int defense = 5;     // ������
    public float moveSpeed = 5f; // �ƶ��ٶ�
}

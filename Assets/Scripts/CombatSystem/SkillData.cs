// SkillData.cs
using UnityEngine;

// ���弼�����ݵ� ScriptableObject
[CreateAssetMenu(fileName = "NewSkill", menuName = "ScriptableObjects/SkillData", order = 1)]
public class SkillData : ScriptableObject
{
    public string skillName; // ��������
    public int damage;       // �����˺�
    public float cooldown;   // ��ȴʱ��
}

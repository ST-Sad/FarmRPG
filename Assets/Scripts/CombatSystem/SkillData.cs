// SkillData.cs
using UnityEngine;

/// <summary>
/// �������ݶ��壬�洢�����˺�����ȴʱ��
/// </summary>
[CreateAssetMenu(fileName = "NewSkill", menuName = "ScriptableObjects/SkillData", order = 1)]
public class SkillData : ScriptableObject
{
    public string skillName; // ��������
    public int damage;       // �˺�ֵ
    public float cooldown;   // ��ȴʱ�䣨�룩
}

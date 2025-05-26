// SkillData.cs
using UnityEngine;

/// <summary>
/// 技能数据定义，存储技能伤害与冷却时间
/// </summary>
[CreateAssetMenu(fileName = "NewSkill", menuName = "ScriptableObjects/SkillData", order = 1)]
public class SkillData : ScriptableObject
{
    public string skillName; // 技能名称
    public int damage;       // 伤害值
    public float cooldown;   // 冷却时间（秒）
}

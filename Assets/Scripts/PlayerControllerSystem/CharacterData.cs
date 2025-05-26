// CharacterData.cs
using UnityEngine;

/// <summary>
/// 角色基础属性数据，用 ScriptableObject 定义（最大生命、攻击力、速度、体力等）
/// </summary>
[CreateAssetMenu(fileName = "NewCharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public int maxHealth = 100;     // 最大生命值
    public int attack = 10;         // 攻击力
    public int defense = 5;         // 防御力
    public float moveSpeed = 5f;    // 移动速度
    public int maxStamina = 100;    // 最大体力
    public float staminaRegen = 5f; // 体力恢复速度
    public float staminaCostPerSecond = 5f; // 移动时每秒消耗的体力
}

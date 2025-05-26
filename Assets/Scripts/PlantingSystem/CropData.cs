// CropData.cs
using UnityEngine;

/// <summary>
/// 作物数据定义，使用 ScriptableObject 存储多阶段生长信息和产出信息:contentReference[oaicite:3]{index=3}
/// </summary>
[CreateAssetMenu(fileName = "NewCropData", menuName = "ScriptableObjects/CropData", order = 1)]
public class CropData : ScriptableObject
{
    public string cropName;               // 作物名称
    public Sprite[] growthStages;         // 各生长阶段的精灵（最后一张为成熟图）
    public float[] stageGrowthTime;       // 对应阶段的生长时间阈值（秒）
    public SeasonManager.Season[] allowedSeasons; // 可种植季节列表
    public ItemData produceItem;          // 收获后产出的物品类型
    public int baseYield = 2;            // 基础产量
    public int fertilizerYieldBonus = 1; // 施肥后的额外产量
    public int expReward = 5;            // 收获时给予玩家的经验
    public GameObject cropPrefab;         // 用于实例化作物的预制件
}

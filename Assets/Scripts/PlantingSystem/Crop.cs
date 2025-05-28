// Crop.cs
using UnityEngine;

/// <summary>
/// 作物行为脚本，根据 CropData 进行分阶段生长，并在成熟后更新精灵:contentReference[oaicite:4]{index=4}
/// </summary>
public class Crop : MonoBehaviour
{
    [HideInInspector] public CropData data; // 作物数据定义
    private FarmTile tile;                 // 所属地块引用
    private SpriteRenderer spriteRenderer;
    private float growthTimer = 0f;        // 当前阶段计时器
    private int currentStage = 0;          // 当前生长阶段索引
    public bool isGrown = false;           // 是否已成熟

    /// <summary>
    /// 初始化作物，设置数据和地块引用，显示初始阶段精灵
    /// </summary>
    public void Initialize(CropData data, FarmTile tile)
    {
        this.data = data;
        this.tile = tile;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        // 设置初始阶段精灵
        if (data.growthStages != null && data.growthStages.Length > 0)
        {
            currentStage = 0;
            spriteRenderer.sprite = data.growthStages[currentStage];
        }
    }

    void Update()
    {
        


        if (isGrown || data.growthStages == null || data.stageGrowthTime == null) return;
        float delta = Time.deltaTime;
        // 根据浇水与施肥情况加速生长
        if (tile.isWatered)
        {
            growthTimer += delta;
            tile.isWatered = false; // 施加一次额外生长后清除标志
        }
        if (tile.isFertilized)
        {
            growthTimer += delta;
            tile.isFertilized = false;
        }
        growthTimer += delta;

        // 达到当前阶段所需时间后切换阶段
        if (growthTimer >= data.stageGrowthTime[currentStage])
        {
            growthTimer = 0f;
            currentStage++;
            if (currentStage < data.growthStages.Length)
            {
                spriteRenderer.sprite = data.growthStages[currentStage];
            }
            // 如果已到达最后一个阶段，则标记成熟
            if (currentStage >= data.growthStages.Length - 1)
            {
                isGrown = true;
            }
        }
    }
}

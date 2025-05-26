// FarmTile.cs
using UnityEngine;

/// <summary>
/// 地块管理，记录是否翻耕、浇水、施肥、是否种植作物等状态，并可翻耕、浇水、施肥、清理地块
/// </summary>
public class FarmTile : MonoBehaviour
{
    [HideInInspector] public bool isPlowed = false;     // 地块是否已翻耕
    [HideInInspector] public bool isWatered = false;    // 本轮是否已浇水
    [HideInInspector] public bool isFertilized = false; // 本轮是否已施肥
    [HideInInspector] public bool isPlanted = false;    // 是否已种植作物
    [HideInInspector] public Crop currentCrop = null;   // 当前生长中的作物

    /// <summary>
    /// 翻耕地块
    /// </summary>
    public void Plow()
    {
        isPlowed = true;
    }

    /// <summary>
    /// 浇水（加速下一阶段生长）
    /// </summary>
    public void Water()
    {
        if (isPlanted)
        {
            isWatered = true;
        }
    }

    /// <summary>
    /// 施肥（加速生长并增加产量）
    /// </summary>
    public void AddFertilizer()
    {
        if (isPlanted)
        {
            isFertilized = true;
        }
    }

    /// <summary>
    /// 在该地块上种植作物（传入作物数据）
    /// </summary>
    public void PlantCrop(CropData cropData)
    {
        if (!isPlowed || isPlanted) return; // 必须已翻耕且无作物才能种植

        // 检查季节是否允许
        if (cropData.allowedSeasons != null && cropData.allowedSeasons.Length > 0)
        {
            bool seasonOK = false;
            foreach (SeasonManager.Season s in cropData.allowedSeasons)
            {
                if (SeasonManager.currentSeason == s) { seasonOK = true; break; }
            }
            if (!seasonOK) return;
        }

        // 实例化作物预制件
        GameObject cropGO = Instantiate(cropData.cropPrefab, transform.position, Quaternion.identity, transform);
        Crop cropScript = cropGO.GetComponent<Crop>();
        cropScript.Initialize(cropData, this); // 初始化作物脚本
        currentCrop = cropScript;
        isPlanted = true;
    }

    /// <summary>
    /// 收获作物：仅在作物成熟时执行。销毁作物实例并重置地块状态
    /// </summary>
    public void HarvestCrop()
    {
        if (!isPlanted || currentCrop == null) return;
        if (currentCrop.isGrown)
        {
            Destroy(currentCrop.gameObject);
            currentCrop = null;
            isPlanted = false;
        }
    }

    /// <summary>
    /// 清理地块：去除未成熟或其他残留物，使地块恢复初始状态
    /// </summary>
    public void ClearTile()
    {
        if (isPlanted && currentCrop != null)
        {
            Destroy(currentCrop.gameObject);
        }
        isPlanted = false;
        currentCrop = null;
        isWatered = false;
        isFertilized = false;
        // 翻耕状态保留或根据设计决定是否重置
    }
}

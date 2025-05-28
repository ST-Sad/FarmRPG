// PlantingManager.cs
using UnityEngine;

/// <summary>
/// 负责玩家对地块的交互：左键种植，右键收获。连接 CropData、FarmTile 和背包、角色经验系统:contentReference[oaicite:5]{index=5}
/// </summary>
public class PlantingManager : MonoBehaviour
{
    [Tooltip("当前选定要种植的作物数据")]
    public CropData selectedCrop;               // 被选中的作物类型
    public InventoryManager inventoryManager;   // 背包管理器（用于收获物品）
    public CharacterStats playerStats;          // 玩家属性（用于收获经验）

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        // 鼠标位置转换为世界坐标
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            FarmTile tile = hit.collider.GetComponent<FarmTile>();

            // 按F键翻耕
            if (Input.GetKeyDown(KeyCode.F) && hit.collider != null)
            {
               
                if (tile != null && !tile.isPlowed)
                {
                    tile.Plow();
                    Debug.Log("翻耕成功！");
                }
            }



            // 左键：种植作物
            if (Input.GetMouseButtonDown(0) && selectedCrop != null && tile != null)
            {
                tile.PlantCrop(selectedCrop);
                Debug.Log("种植尝试...");
            }
            // 右键：收获成熟作物
            if (Input.GetMouseButtonDown(1) && tile != null && tile.isPlanted && tile.currentCrop != null)
            {
                CropData data = tile.currentCrop.data;
                if (tile.currentCrop.isGrown)
                {
                    // 计算产量：基础产量 + 施肥加成
                    int yield = data.baseYield;
                    if (tile.isFertilized) yield += data.fertilizerYieldBonus;
                    // 给玩家背包和经验
                    if (inventoryManager != null && data.produceItem != null)
                        inventoryManager.AddItem(data.produceItem, yield);
                    if (playerStats != null)
                        playerStats.AddExp(data.expReward * yield);
                    // 收获并销毁作物
                    tile.HarvestCrop();
                    tile.isFertilized = false;
                }
            }
        }
    }
}

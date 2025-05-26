// InventoryUI.cs
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 背包界面，根据 InventoryManager.slots 动态生成 UI 槽位:contentReference[oaicite:11]{index=11}
/// </summary>
public class InventoryUI : MonoBehaviour
{
    public InventoryManager inventoryManager;  // 背包管理器引用
    public Transform slotsParent;              // UI 槽父物体，用于布局
    public GameObject slotPrefab;              // 单个物品槽预制件（包含 Image 和 Text）

    void Start()
    {
        RefreshUI();
    }

    /// <summary>
    /// 刷新背包 UI：清除旧的槽位，重新创建所有物品槽
    /// </summary>
    public void RefreshUI()
    {
        // 删除旧的 UI 槽
        foreach (Transform child in slotsParent)
        {
            Destroy(child.gameObject);
        }
        // 根据背包数据创建新的 UI 槽
        foreach (InventorySlot slot in inventoryManager.slots)
        {
            GameObject go = Instantiate(slotPrefab, slotsParent);
            Image icon = go.transform.Find("Icon").GetComponent<Image>();
            Text qtyText = go.transform.Find("Quantity").GetComponent<Text>();
            if (icon != null) icon.sprite = slot.item.icon;
            if (qtyText != null) qtyText.text = slot.quantity.ToString();
        }
    }
}

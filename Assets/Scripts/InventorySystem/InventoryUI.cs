using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public InventoryManager inventoryManager;  // 背包管理器引用
    public Transform slotsParent;              // UI 槽父物体，用于布局
    public GameObject slotPrefab;              // 单个物品槽预制件（包含 Image 和 Text）
    public int totalSlotCount = 21;           // 修改：固定总槽位数（取代 emptySlotCount）

    void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        // 删除旧的 UI 槽
        foreach (Transform child in slotsParent)
        {
            Destroy(child.gameObject);
        }

        // 生成物品槽位，补齐空槽位到 totalSlotCount
        int slotsToCreate = Mathf.Min(inventoryManager.slots.Count, totalSlotCount);
        // 先创建现有物品的槽位
        for (int i = 0; i < slotsToCreate; i++)
        {
            InventorySlot slot = inventoryManager.slots[i];
            GameObject go = Instantiate(slotPrefab, slotsParent);
            Image icon = go.transform.Find("Icon")?.GetComponent<Image>();
            TextMeshProUGUI qtyTextPro = go.transform.Find("Quantity")?.GetComponent<TextMeshProUGUI>();
            Text qtyText = go.transform.Find("Quantity")?.GetComponent<Text>();
            if (icon != null)icon.sprite = slot.item.icon;
            if (qtyTextPro != null) qtyTextPro.text = slot.quantity.ToString();
            else if (qtyText != null) qtyText.text = slot.quantity.ToString();
        }

        // 补齐剩余的空槽位
        for (int i = slotsToCreate; i < totalSlotCount; i++)
        {
            GameObject go = Instantiate(slotPrefab, slotsParent);
            Image icon = go.transform.Find("Icon")?.GetComponent<Image>();
            TextMeshProUGUI qtyTextPro = go.transform.Find("Quantity")?.GetComponent<TextMeshProUGUI>();
            Text qtyText = go.transform.Find("Quantity")?.GetComponent<Text>();
            if (icon != null)
            {
                icon.sprite = null; // 空槽位无图标
                Color iconColor = icon.color;
                iconColor.a = 0f; // 设置透明度为 0
                icon.color = iconColor;
            }
                if (qtyTextPro != null) qtyTextPro.text = ""; // 数量显示为空
            else if (qtyText != null) qtyText.text = "0";
        }
    }
}
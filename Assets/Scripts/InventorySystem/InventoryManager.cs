// InventoryManager.cs
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// 管理背包物品列表，包括添加、移除、使用、保存与加载功能
/// </summary>
public class InventoryManager : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>(); // 物品槽列表

    /// <summary>
    /// 添加物品到背包（如果可堆叠则增加数量，否则创建新槽）
    void Start()
    {
        // 创建测试物品（需提前在 Resources 目录创建 ItemData 资源）
        ItemData potato = Resources.Load<ItemData>("potato");
        ItemData onion = Resources.Load<ItemData>("onion");
        AddItem(potato, 5);
        AddItem(onion, 3); 
    }
    /// </summary>
    public void AddItem(ItemData item, int count = 1)
    {
        if (item == null || count <= 0) return;
        if (item.isStackable)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.item == item)
                {
                    slot.quantity += count;
                    return;
                }
            }
        }
        slots.Add(new InventorySlot(item, count));
    }

    /// <summary>
    /// 从背包中移除物品指定数量，如果数量为0则移除槽
    /// </summary>
    public void RemoveItem(ItemData item, int count = 1)
    {
        if (item == null || count <= 0) return;
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == item)
            {
                slots[i].quantity -= count;
                if (slots[i].quantity <= 0)
                {
                    slots.RemoveAt(i);
                }
                return;
            }
        }
    }

    /// <summary>
    /// 使用物品的接口，具体功能可扩展（例如使用种子种植、药水回复生命等）
    /// </summary>
    public void UseItem(ItemData item)
    {
        if (item == null) return;
        // 示例：如果是种子，调用种植逻辑；如果是药水，恢复玩家生命等。
        // 具体实现根据游戏设计扩展
        RemoveItem(item, 1);
    }

    /// <summary>
    /// 将背包数据保存到本地文件（使用 JSON 格式）
    /// </summary>
    public void SaveInventory()
    {
        InventorySaveData saveData = new InventorySaveData();
        saveData.slots = new List<InventorySaveSlot>();
        foreach (InventorySlot slot in slots)
        {
            InventorySaveSlot saveSlot = new InventorySaveSlot();
            saveSlot.itemName = slot.item.itemName;
            saveSlot.quantity = slot.quantity;
            saveData.slots.Add(saveSlot);
        }
        string json = JsonUtility.ToJson(saveData, true);
        string path = Path.Combine(Application.persistentDataPath, "inventory.json");
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// 从本地文件加载背包数据（需将 ItemData 资源放在 Resources 目录下，以通过名称加载）
    /// </summary>
    public void LoadInventory()
    {
        string path = Path.Combine(Application.persistentDataPath, "inventory.json");
        if (!File.Exists(path)) return;
        string json = File.ReadAllText(path);
        InventorySaveData saveData = JsonUtility.FromJson<InventorySaveData>(json);
        slots.Clear();
        foreach (InventorySaveSlot saveSlot in saveData.slots)
        {
            // 通过名称加载 ItemData 资源
            ItemData item = Resources.Load<ItemData>(saveSlot.itemName);
            if (item != null)
            {
                slots.Add(new InventorySlot(item, saveSlot.quantity));
            }
        }
    }

    // 辅助类用于 JSON 序列化背包数据
    [System.Serializable]
    private class InventorySaveData
    {
        public List<InventorySaveSlot> slots;
    }
    [System.Serializable]
    private class InventorySaveSlot
    {
        public string itemName;
        public int quantity;
    }
}
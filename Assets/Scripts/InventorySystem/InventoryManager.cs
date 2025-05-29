using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System; // 新增：支持事件

public class InventoryManager : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>(); // 物品槽列表
    public event Action OnInventoryChanged; // 新增：背包变化事件

    void Start()
    {
        ItemData potato = Resources.Load<ItemData>("potato");
        ItemData onion = Resources.Load<ItemData>("onion");
        AddItem(potato, 5);
        AddItem(potato, 3); // 叠加后数量为 8
        AddItem(onion, 88);
        DontDestroyOnLoad(gameObject);
    }


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
                    OnInventoryChanged?.Invoke(); // 新增：通知 UI 刷新
                    return;
                }
            }
        }
        QuestManager.Instance.UpdateQuestProgress(
               QuestObjectiveType.Collect,
               item.itemName,
               1
           );
        slots.Add(new InventorySlot(item, count));
        OnInventoryChanged?.Invoke(); // 新增：通知 UI 刷新
    }

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
                OnInventoryChanged?.Invoke(); // 新增：通知 UI 刷新
                return;
            }
        }
    }

    public void UseItem(ItemData item)
    {
        if (item == null) return;
        RemoveItem(item, 1);
        // OnInventoryChanged?.Invoke(); // RemoveItem 已触发，无需重复
    }

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

    public void LoadInventory()
    {
        string path = Path.Combine(Application.persistentDataPath, "inventory.json");
        if (!File.Exists(path)) return;
        string json = File.ReadAllText(path);
        InventorySaveData saveData = JsonUtility.FromJson<InventorySaveData>(json);
        slots.Clear();
        foreach (InventorySaveSlot saveSlot in saveData.slots)
        {
            ItemData item = Resources.Load<ItemData>(saveSlot.itemName);
            if (item != null)
            {
                slots.Add(new InventorySlot(item, saveSlot.quantity));
            }
        }
        OnInventoryChanged?.Invoke(); // 新增：加载后刷新 UI
    }

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
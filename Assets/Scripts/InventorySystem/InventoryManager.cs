// InventoryManager.cs
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// ��������Ʒ�б�������ӡ��Ƴ���ʹ�á���������ع���
/// </summary>
public class InventoryManager : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>(); // ��Ʒ���б�

    /// <summary>
    /// �����Ʒ������������ɶѵ����������������򴴽��²ۣ�
    void Start()
    {
        // ����������Ʒ������ǰ�� Resources Ŀ¼���� ItemData ��Դ��
        ItemData potato = Resources.Load<ItemData>("potato");
        AddItem(potato, 5);
        AddItem(potato, 3); // ���Ӻ�����Ϊ 8
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
    /// �ӱ������Ƴ���Ʒָ���������������Ϊ0���Ƴ���
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
    /// ʹ����Ʒ�Ľӿڣ����幦�ܿ���չ������ʹ��������ֲ��ҩˮ�ظ������ȣ�
    /// </summary>
    public void UseItem(ItemData item)
    {
        if (item == null) return;
        // ʾ������������ӣ�������ֲ�߼��������ҩˮ���ָ���������ȡ�
        // ����ʵ�ָ�����Ϸ�����չ
        RemoveItem(item, 1);
    }

    /// <summary>
    /// ���������ݱ��浽�����ļ���ʹ�� JSON ��ʽ��
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
    /// �ӱ����ļ����ر������ݣ��轫 ItemData ��Դ���� Resources Ŀ¼�£���ͨ�����Ƽ��أ�
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
            // ͨ�����Ƽ��� ItemData ��Դ
            ItemData item = Resources.Load<ItemData>(saveSlot.itemName);
            if (item != null)
            {
                slots.Add(new InventorySlot(item, saveSlot.quantity));
            }
        }
    }

    // ���������� JSON ���л���������
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
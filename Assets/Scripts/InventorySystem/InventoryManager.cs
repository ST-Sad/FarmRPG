// InventoryManager.cs
using System.Collections.Generic;
using UnityEngine;

// ��������Ʒ�б�
public class InventoryManager : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>(); // ��Ʒ���б�

    // �����Ʒ
    public void AddItem(ItemData item, int count = 1)
    {
        if (item.isStackable)
        {
            // ����Ƿ�������ͬ��Ʒ�����Ե���
            foreach (InventorySlot slot in slots)
            {
                if (slot.item == item)
                {
                    slot.quantity += count;
                    return;
                }
            }
        }
        // �����Ʒ���ɵ��ӻ��б��в����ڣ��򴴽��²�
        slots.Add(new InventorySlot(item, count));
    }

    // �Ƴ���Ʒ
    public void RemoveItem(ItemData item, int count = 1)
    {
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
}

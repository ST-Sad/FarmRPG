// InventoryUI.cs
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������棬���� InventoryManager.slots ��̬���� UI ��λ:contentReference[oaicite:11]{index=11}
/// </summary>
public class InventoryUI : MonoBehaviour
{
    public InventoryManager inventoryManager;  // ��������������
    public Transform slotsParent;              // UI �۸����壬���ڲ���
    public GameObject slotPrefab;              // ������Ʒ��Ԥ�Ƽ������� Image �� Text��

    void Start()
    {
        RefreshUI();
    }

    /// <summary>
    /// ˢ�±��� UI������ɵĲ�λ�����´���������Ʒ��
    /// </summary>
    public void RefreshUI()
    {
        // ɾ���ɵ� UI ��
        foreach (Transform child in slotsParent)
        {
            Destroy(child.gameObject);
        }
        // ���ݱ������ݴ����µ� UI ��
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

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public InventoryManager inventoryManager;  // ��������������
    public Transform slotsParent;              // UI �۸����壬���ڲ���
    public GameObject slotPrefab;              // ������Ʒ��Ԥ�Ƽ������� Image �� Text��
    public int totalSlotCount = 21;           // �޸ģ��̶��ܲ�λ����ȡ�� emptySlotCount��

    void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        // ɾ���ɵ� UI ��
        foreach (Transform child in slotsParent)
        {
            Destroy(child.gameObject);
        }

        // ������Ʒ��λ������ղ�λ�� totalSlotCount
        int slotsToCreate = Mathf.Min(inventoryManager.slots.Count, totalSlotCount);
        // �ȴ���������Ʒ�Ĳ�λ
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

        // ����ʣ��Ŀղ�λ
        for (int i = slotsToCreate; i < totalSlotCount; i++)
        {
            GameObject go = Instantiate(slotPrefab, slotsParent);
            Image icon = go.transform.Find("Icon")?.GetComponent<Image>();
            TextMeshProUGUI qtyTextPro = go.transform.Find("Quantity")?.GetComponent<TextMeshProUGUI>();
            Text qtyText = go.transform.Find("Quantity")?.GetComponent<Text>();
            if (icon != null)
            {
                icon.sprite = null; // �ղ�λ��ͼ��
                Color iconColor = icon.color;
                iconColor.a = 0f; // ����͸����Ϊ 0
                icon.color = iconColor;
            }
                if (qtyTextPro != null) qtyTextPro.text = ""; // ������ʾΪ��
            else if (qtyText != null) qtyText.text = "0";
        }
    }
}
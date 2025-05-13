// InventoryUI.cs
using UnityEngine;
using UnityEngine.UI;

// ���� InventoryManager �������ɱ�������
public class InventoryUI : MonoBehaviour
{
    public InventoryManager inventoryManager; // ����������
    public Transform slotsParent;             // UI�۵ĸ����壨���ڲ��֣�
    public GameObject slotPrefab;             // ������Ʒ��Ԥ����

    void Start()
    {
        RefreshUI();
    }

    // ˢ�±���UI
    public void RefreshUI()
    {
        // ɾ���ɵ�UI��
        foreach (Transform child in slotsParent)
        {
            Destroy(child.gameObject);
        }
        // ����������Ʒ�۲�����UIԪ��
        foreach (InventorySlot slot in inventoryManager.slots)
        {
            GameObject go = Instantiate(slotPrefab, slotsParent);
            // ����slotPrefab�������Ӷ���Image������ʾͼ�꣬Text������ʾ����
            Image icon = go.transform.Find("Icon").GetComponent<Image>();
            Text qtyText = go.transform.Find("Quantity").GetComponent<Text>();
            icon.sprite = slot.item.icon;
            qtyText.text = slot.quantity.ToString();
        }
    }
}

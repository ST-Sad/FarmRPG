// InventorySlot.cs
// �洢������Ʒ�����������ݽṹ
[System.Serializable]
public class InventorySlot
{
    public ItemData item; // ��Ʒ����
    public int quantity;  // ����

    public InventorySlot(ItemData item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}

// InventorySlot.cs
/// <summary>
/// ������λ���ݽṹ���洢��Ʒ������:contentReference[oaicite:10]{index=10}
/// </summary>
[System.Serializable]
public class InventorySlot
{
    public ItemData item;   // ��Ʒ����
    public int quantity;    // ����

    public InventorySlot(ItemData item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}

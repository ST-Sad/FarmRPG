// InventorySlot.cs
/// <summary>
/// 背包槽位数据结构，存储物品和数量:contentReference[oaicite:10]{index=10}
/// </summary>
[System.Serializable]
public class InventorySlot
{
    public ItemData item;   // 物品数据
    public int quantity;    // 数量

    public InventorySlot(ItemData item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}

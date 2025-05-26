// ItemData.cs
using UnityEngine;

/// <summary>
/// 物品数据定义，使用 ScriptableObject 存储物品名称、图标等信息:contentReference[oaicite:9]{index=9}
/// </summary>
[CreateAssetMenu(fileName = "NewItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;    // 物品名称（应与资源文件名一致，用于保存/加载）
    public Sprite icon;        // 物品图标
    public bool isStackable = true; // 是否可堆叠
    // 可根据需要添加物品类型、用途等字段
}

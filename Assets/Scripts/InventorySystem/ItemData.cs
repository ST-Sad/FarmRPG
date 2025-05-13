// ItemData.cs
using UnityEngine;

// ������Ʒ���ݵ� ScriptableObject
[CreateAssetMenu(fileName = "NewItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;   // ��Ʒ����
    public Sprite icon;       // ��Ʒͼ��
    public bool isStackable = true; // �Ƿ�ɵ���
}

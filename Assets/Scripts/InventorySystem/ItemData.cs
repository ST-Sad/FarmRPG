// ItemData.cs
using UnityEngine;

/// <summary>
/// ��Ʒ���ݶ��壬ʹ�� ScriptableObject �洢��Ʒ���ơ�ͼ�����Ϣ:contentReference[oaicite:9]{index=9}
/// </summary>
[CreateAssetMenu(fileName = "NewItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;    // ��Ʒ���ƣ�Ӧ����Դ�ļ���һ�£����ڱ���/���أ�
    public Sprite icon;        // ��Ʒͼ��
    public bool isStackable = true; // �Ƿ�ɶѵ�
    // �ɸ�����Ҫ�����Ʒ���͡���;���ֶ�
}

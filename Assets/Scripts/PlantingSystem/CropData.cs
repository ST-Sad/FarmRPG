// CropData.cs
using UnityEngine;

// �����������ݵ� ScriptableObject
[CreateAssetMenu(fileName = "NewCropData", menuName = "ScriptableObjects/CropData", order = 1)]
public class CropData : ScriptableObject
{
    public string cropName; // ��������
    public float growthTime; // �ɳ�ʱ�䣨�룩
    public Sprite initialSprite; // ����ʱ��ʾ�ľ���
    public Sprite grownSprite; // ����ʱ��ʾ�ľ���
    public GameObject cropPrefab; // ����ʵ���������Ԥ�Ƽ�

    // �����ֶδ洢����Ĺ�������:contentReference[oaicite:1]{index=1}��
}

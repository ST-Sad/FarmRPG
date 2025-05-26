// CropData.cs
using UnityEngine;

/// <summary>
/// �������ݶ��壬ʹ�� ScriptableObject �洢��׶�������Ϣ�Ͳ�����Ϣ:contentReference[oaicite:3]{index=3}
/// </summary>
[CreateAssetMenu(fileName = "NewCropData", menuName = "ScriptableObjects/CropData", order = 1)]
public class CropData : ScriptableObject
{
    public string cropName;               // ��������
    public Sprite[] growthStages;         // �������׶εľ��飨���һ��Ϊ����ͼ��
    public float[] stageGrowthTime;       // ��Ӧ�׶ε�����ʱ����ֵ���룩
    public SeasonManager.Season[] allowedSeasons; // ����ֲ�����б�
    public ItemData produceItem;          // �ջ���������Ʒ����
    public int baseYield = 2;            // ��������
    public int fertilizerYieldBonus = 1; // ʩ�ʺ�Ķ������
    public int expReward = 5;            // �ջ�ʱ������ҵľ���
    public GameObject cropPrefab;         // ����ʵ���������Ԥ�Ƽ�
}

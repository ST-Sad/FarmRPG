// Crop.cs
using UnityEngine;

/// <summary>
/// ������Ϊ�ű������� CropData ���зֽ׶����������ڳ������¾���:contentReference[oaicite:4]{index=4}
/// </summary>
public class Crop : MonoBehaviour
{
    [HideInInspector] public CropData data; // �������ݶ���
    private FarmTile tile;                 // �����ؿ�����
    private SpriteRenderer spriteRenderer;
    private float growthTimer = 0f;        // ��ǰ�׶μ�ʱ��
    private int currentStage = 0;          // ��ǰ�����׶�����
    public bool isGrown = false;           // �Ƿ��ѳ���

    /// <summary>
    /// ��ʼ������������ݺ͵ؿ����ã���ʾ��ʼ�׶ξ���
    /// </summary>
    public void Initialize(CropData data, FarmTile tile)
    {
        this.data = data;
        this.tile = tile;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        // ���ó�ʼ�׶ξ���
        if (data.growthStages != null && data.growthStages.Length > 0)
        {
            currentStage = 0;
            spriteRenderer.sprite = data.growthStages[currentStage];
        }
    }

    void Update()
    {
        if (isGrown || data.growthStages == null || data.stageGrowthTime == null) return;
        float delta = Time.deltaTime;
        // ���ݽ�ˮ��ʩ�������������
        if (tile.isWatered)
        {
            growthTimer += delta;
            tile.isWatered = false; // ʩ��һ�ζ��������������־
        }
        if (tile.isFertilized)
        {
            growthTimer += delta;
            tile.isFertilized = false;
        }
        growthTimer += delta;

        // �ﵽ��ǰ�׶�����ʱ����л��׶�
        if (growthTimer >= data.stageGrowthTime[currentStage])
        {
            growthTimer = 0f;
            currentStage++;
            if (currentStage < data.growthStages.Length)
            {
                spriteRenderer.sprite = data.growthStages[currentStage];
            }
            // ����ѵ������һ���׶Σ����ǳ���
            if (currentStage >= data.growthStages.Length - 1)
            {
                isGrown = true;
            }
        }
    }
}

// Crop.cs
using UnityEngine;

public class Crop : MonoBehaviour
{
    [HideInInspector]
    public CropData data; // ��������ݶ���
    private FarmTile tile; // �����ؿ�
    private float growthTimer = 0f; // �ɳ���ʱ��
    public bool isGrown = false; // �Ƿ��ѳ���
    private SpriteRenderer spriteRenderer;

    // ��ʼ����������
    public void Initialize(CropData data, FarmTile tile)
    {
        this.data = data;
        this.tile = tile;
        // ��ȡ�����SpriteRenderer������ʾ����
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        // ���ó�ʼ�׶εľ���
        if (data.initialSprite != null)
            spriteRenderer.sprite = data.initialSprite;
    }

    void Update()
    {
        // ���δ���죬�����ӳɳ�ʱ��
        if (!isGrown)
        {
            growthTimer += Time.deltaTime;
            // �ﵽ����ʱ����л�Ϊ����״̬
            if (growthTimer >= data.growthTime)
            {
                isGrown = true;
                if (data.grownSprite != null)
                    spriteRenderer.sprite = data.grownSprite; // �л����쾫��
            }
        }
    }
}

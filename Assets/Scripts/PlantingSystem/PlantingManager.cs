// PlantingManager.cs
using UnityEngine;

/// <summary>
/// ������ҶԵؿ�Ľ����������ֲ���Ҽ��ջ����� CropData��FarmTile �ͱ�������ɫ����ϵͳ:contentReference[oaicite:5]{index=5}
/// </summary>
public class PlantingManager : MonoBehaviour
{
    [Tooltip("��ǰѡ��Ҫ��ֲ����������")]
    public CropData selectedCrop;               // ��ѡ�е���������
    public InventoryManager inventoryManager;   // �����������������ջ���Ʒ��
    public CharacterStats playerStats;          // ������ԣ������ջ��飩

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        // ���λ��ת��Ϊ��������
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            FarmTile tile = hit.collider.GetComponent<FarmTile>();

            // ��F������
            if (Input.GetKeyDown(KeyCode.F) && hit.collider != null)
            {
               
                if (tile != null && !tile.isPlowed)
                {
                    tile.Plow();
                    Debug.Log("�����ɹ���");
                }
            }



            // �������ֲ����
            if (Input.GetMouseButtonDown(0) && selectedCrop != null && tile != null)
            {
                tile.PlantCrop(selectedCrop);
                Debug.Log("��ֲ����...");
            }
            // �Ҽ����ջ��������
            if (Input.GetMouseButtonDown(1) && tile != null && tile.isPlanted && tile.currentCrop != null)
            {
                CropData data = tile.currentCrop.data;
                if (tile.currentCrop.isGrown)
                {
                    // ����������������� + ʩ�ʼӳ�
                    int yield = data.baseYield;
                    if (tile.isFertilized) yield += data.fertilizerYieldBonus;
                    // ����ұ����;���
                    if (inventoryManager != null && data.produceItem != null)
                        inventoryManager.AddItem(data.produceItem, yield);
                    if (playerStats != null)
                        playerStats.AddExp(data.expReward * yield);
                    // �ջ���������
                    tile.HarvestCrop();
                    tile.isFertilized = false;
                }
            }
        }
    }
}

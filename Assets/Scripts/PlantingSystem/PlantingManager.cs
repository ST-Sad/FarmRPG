// PlantingManager.cs
using UnityEngine;
using System.Collections.Generic;
public class PlantingManager : MonoBehaviour
{
    [Tooltip("��ǰѡ������������")]
    public CropData selectedCrop; // ��ѡ�е���������
    List<FarmTile> farmTiles;
    void Update()
    {

        // ��������⣺�ڱ�����ĵؿ�����ֲ����
        if (Input.GetMouseButtonDown(0) && selectedCrop != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                FarmTile tile = hit.collider.GetComponent<FarmTile>();
                if (tile != null && !tile.isPlanted)
                {
                    tile.PlantCrop(selectedCrop);
                }
            }
        }
    }
}

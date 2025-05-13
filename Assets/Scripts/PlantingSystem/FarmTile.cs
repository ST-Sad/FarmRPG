// FarmTile.cs
using UnityEngine;

public class FarmTile : MonoBehaviour
{
    [HideInInspector]
    public bool isPlanted = false; // �ؿ��Ƿ��ѱ���ֲ
    [HideInInspector]
    public Crop currentCrop; // ��ǰ��ֲ������ʵ��

    // �ڵؿ�����ֲ����
    public void PlantCrop(CropData cropData)
    {
        if (isPlanted) return; // ����ֲ��ִ��

        // ʹ�����������е�Ԥ�Ƽ��ڴ˵ؿ�λ��ʵ��������
        GameObject cropGO = Instantiate(cropData.cropPrefab, transform.position, Quaternion.identity, transform);
        Crop cropScript = cropGO.GetComponent<Crop>();
        cropScript.Initialize(cropData, this); // ��ʼ������ű�
        currentCrop = cropScript;
        isPlanted = true;
    }

    // �ջ�����Ƴ�����ʵ��
    public void HarvestCrop()
    {
        if (!isPlanted || currentCrop == null) return;
        if (currentCrop.isGrown)
        {
            Destroy(currentCrop.gameObject);
            currentCrop = null;
            isPlanted = false;
        }
    }
}

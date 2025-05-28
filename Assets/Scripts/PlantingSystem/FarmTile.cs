// FarmTile.cs
using UnityEngine;

/// <summary>
/// �ؿ������¼�Ƿ񷭸�����ˮ��ʩ�ʡ��Ƿ���ֲ�����״̬�����ɷ�������ˮ��ʩ�ʡ�����ؿ�
/// </summary>
public class FarmTile : MonoBehaviour
{
    [HideInInspector] public bool isPlowed = false;     // �ؿ��Ƿ��ѷ���
    [HideInInspector] public bool isWatered = false;    // �����Ƿ��ѽ�ˮ
    [HideInInspector] public bool isFertilized = false; // �����Ƿ���ʩ��
    [HideInInspector] public bool isPlanted = false;    // �Ƿ�����ֲ����
    [HideInInspector] public Crop currentCrop = null;   // ��ǰ�����е�����

    /// <summary>
    /// �����ؿ�
    /// </summary>
    public void Plow()
    {
        isPlowed = true;
    }

    /// <summary>
    /// ��ˮ��������һ�׶�������
    /// </summary>
    public void Water()
    {
        if (isPlanted)
        {
            isWatered = true;
        }
    }

    /// <summary>
    /// ʩ�ʣ��������������Ӳ�����
    /// </summary>
    public void AddFertilizer()
    {
        if (isPlanted)
        {
            isFertilized = true;
        }
    }

    /// <summary>
    /// �ڸõؿ�����ֲ��������������ݣ�
    /// </summary>
    public void PlantCrop(CropData cropData)
    {
        if (!isPlowed || isPlanted)
        {   
            Debug.Log($"��ֹ��ֲ��isPlowed={isPlowed}, isPlanted={isPlanted}");
            return; // �����ѷ����������������ֲ
            
        }

            // ��鼾���Ƿ�����
            if (cropData.allowedSeasons != null && cropData.allowedSeasons.Length > 0)
        {
            bool seasonOK = false;
            foreach (SeasonManager.Season s in cropData.allowedSeasons)
            {
                if (SeasonManager.currentSeason == s) { seasonOK = true; break; }
            }
            if (!seasonOK) return;
        }

        // ʵ��������Ԥ�Ƽ�
        GameObject cropGO = Instantiate(cropData.cropPrefab, transform.position, Quaternion.identity, transform);
        Crop cropScript = cropGO.GetComponent<Crop>();
        cropScript.Initialize(cropData, this); // ��ʼ������ű�
        currentCrop = cropScript;
        isPlanted = true;
    }

    /// <summary>
    /// �ջ���������������ʱִ�С���������ʵ�������õؿ�״̬
    /// </summary>
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

    /// <summary>
    /// ����ؿ飺ȥ��δ��������������ʹ�ؿ�ָ���ʼ״̬
    /// </summary>
    public void ClearTile()
    {
        if (isPlanted && currentCrop != null)
        {
            Destroy(currentCrop.gameObject);
        }
        isPlanted = false;
        currentCrop = null;
        isWatered = false;
        isFertilized = false;
        // ����״̬�����������ƾ����Ƿ�����
    }
}

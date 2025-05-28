using UnityEngine;

[RequireComponent(typeof(FarmTile))]
public class FarmTileVisual : MonoBehaviour
{
    [Tooltip("δ����ʱ�ĵؿ���ͼ")]
    public Sprite unplowedSprite;
    [Tooltip("������ĵؿ���ͼ")]
    public Sprite plowedSprite;
    [Tooltip("�ղ�ժ���Ԥ�Ƽ���С�ӣ�")]
    public GameObject harvestedPitPrefab;
    [Tooltip("�ղ�ժ��״̬��ʾʱ�䣨�룩")]
    public float harvestedDisplayTime = 3f;
    [Tooltip("С��λ��ƫ�ƣ�����ڵؿ����ģ�")]
    public Vector2 pitOffset = new Vector2(0f, 0f); // Ĭ����ƫ�ƣ�����������

    private SpriteRenderer spriteRenderer;
    private FarmTile farmTile;
    private bool wasPlantedLastFrame;
    private bool isShowingHarvested;
    private GameObject currentPit;
    private bool lastPlowedState;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        farmTile = GetComponent<FarmTile>();
        wasPlantedLastFrame = farmTile.isPlanted;
        lastPlowedState = farmTile.isPlowed;

        if (spriteRenderer == null)
        {
            Debug.LogError($"FarmTileVisual ({gameObject.name}): ȱ�� SpriteRenderer���Զ���ӡ�");
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sortingLayerName = "Tile";
            spriteRenderer.sortingOrder = 0;
        }

        UpdateTileSprite();
    }

    void Update()
    {
        if (wasPlantedLastFrame && !farmTile.isPlanted && farmTile.currentCrop == null && !isShowingHarvested)
        {
            Debug.Log($"FarmTileVisual ({gameObject.name}): ��⵽��ժ����ʼ��ʾС��");
            StartCoroutine(ShowHarvestedPit());
        }
        wasPlantedLastFrame = farmTile.isPlanted;

        if (farmTile.isPlowed != lastPlowedState && !isShowingHarvested)
        {
            UpdateTileSprite();
            lastPlowedState = farmTile.isPlowed;
        }
    }

    private void UpdateTileSprite()
    {
        if (spriteRenderer == null || unplowedSprite == null || plowedSprite == null)
        {
            Debug.LogWarning($"FarmTileVisual ({gameObject.name}): ȱ�� SpriteRenderer ����ͼ���ã�");
            return;
        }
        spriteRenderer.sprite = farmTile.isPlowed ? plowedSprite : unplowedSprite;
        Debug.Log($"FarmTileVisual ({gameObject.name}): isPlowed={farmTile.isPlowed}, Sprite={spriteRenderer.sprite?.name}");
    }

    private System.Collections.IEnumerator ShowHarvestedPit()
    {
        if (harvestedPitPrefab == null)
        {
            Debug.LogWarning($"FarmTileVisual ({gameObject.name}): HarvestedPitPrefab δ��ֵ��");
            yield break;
        }

        isShowingHarvested = true;
        // ����С��λ�ã�ȷ������
        Vector3 tileCenter = transform.position;
        // ����ؿ� Sprite �� Pivot �� Center���������ƫ�ƣ�������� pitOffset ����
        Vector3 pitPosition = tileCenter + new Vector3(pitOffset.x, pitOffset.y, 0);
        Debug.Log($"FarmTileVisual ({gameObject.name}): �� {pitPosition} ʵ����С�ӣ��ؿ�λ��={tileCenter}");
        currentPit = Instantiate(harvestedPitPrefab, pitPosition, Quaternion.identity, transform);
        SpriteRenderer pitRenderer = currentPit.GetComponent<SpriteRenderer>();
        if (pitRenderer != null)
        {
            pitRenderer.sortingLayerName = "Crop";
            pitRenderer.sortingOrder = 1;
            Debug.Log($"FarmTileVisual ({gameObject.name}): С�� SortingLayer={pitRenderer.sortingLayerName}, SortingOrder={pitRenderer.sortingOrder}");
        }
        else
        {
            Debug.LogWarning($"FarmTileVisual ({gameObject.name}): HarvestedPitPrefab ȱ�� SpriteRenderer��");
        }

        yield return new WaitForSeconds(harvestedDisplayTime);

        if (currentPit != null)
        {
            Destroy(currentPit);
            Debug.Log($"FarmTileVisual ({gameObject.name}): ����С��");
        }
        farmTile.isPlowed = false;
        UpdateTileSprite();
        isShowingHarvested = false;
    }

    void OnDestroy()
    {
        if (currentPit != null)
        {
            Destroy(currentPit);
        }
    }
}
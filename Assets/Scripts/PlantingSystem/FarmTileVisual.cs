using UnityEngine;

[RequireComponent(typeof(FarmTile))]
public class FarmTileVisual : MonoBehaviour
{
    [Tooltip("未翻耕时的地块贴图")]
    public Sprite unplowedSprite;
    [Tooltip("翻耕后的地块贴图")]
    public Sprite plowedSprite;
    [Tooltip("刚采摘完的预制件（小坑）")]
    public GameObject harvestedPitPrefab;
    [Tooltip("刚采摘完状态显示时间（秒）")]
    public float harvestedDisplayTime = 3f;
    [Tooltip("小坑位置偏移（相对于地块中心）")]
    public Vector2 pitOffset = new Vector2(0f, 0f); // 默认无偏移，调整至中心

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
            Debug.LogError($"FarmTileVisual ({gameObject.name}): 缺少 SpriteRenderer！自动添加。");
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
            Debug.Log($"FarmTileVisual ({gameObject.name}): 检测到采摘，开始显示小坑");
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
            Debug.LogWarning($"FarmTileVisual ({gameObject.name}): 缺少 SpriteRenderer 或贴图配置！");
            return;
        }
        spriteRenderer.sprite = farmTile.isPlowed ? plowedSprite : unplowedSprite;
        Debug.Log($"FarmTileVisual ({gameObject.name}): isPlowed={farmTile.isPlowed}, Sprite={spriteRenderer.sprite?.name}");
    }

    private System.Collections.IEnumerator ShowHarvestedPit()
    {
        if (harvestedPitPrefab == null)
        {
            Debug.LogWarning($"FarmTileVisual ({gameObject.name}): HarvestedPitPrefab 未赋值！");
            yield break;
        }

        isShowingHarvested = true;
        // 计算小坑位置，确保居中
        Vector3 tileCenter = transform.position;
        // 如果地块 Sprite 的 Pivot 是 Center，无需额外偏移；否则根据 pitOffset 调整
        Vector3 pitPosition = tileCenter + new Vector3(pitOffset.x, pitOffset.y, 0);
        Debug.Log($"FarmTileVisual ({gameObject.name}): 在 {pitPosition} 实例化小坑，地块位置={tileCenter}");
        currentPit = Instantiate(harvestedPitPrefab, pitPosition, Quaternion.identity, transform);
        SpriteRenderer pitRenderer = currentPit.GetComponent<SpriteRenderer>();
        if (pitRenderer != null)
        {
            pitRenderer.sortingLayerName = "Crop";
            pitRenderer.sortingOrder = 1;
            Debug.Log($"FarmTileVisual ({gameObject.name}): 小坑 SortingLayer={pitRenderer.sortingLayerName}, SortingOrder={pitRenderer.sortingOrder}");
        }
        else
        {
            Debug.LogWarning($"FarmTileVisual ({gameObject.name}): HarvestedPitPrefab 缺少 SpriteRenderer！");
        }

        yield return new WaitForSeconds(harvestedDisplayTime);

        if (currentPit != null)
        {
            Destroy(currentPit);
            Debug.Log($"FarmTileVisual ({gameObject.name}): 销毁小坑");
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
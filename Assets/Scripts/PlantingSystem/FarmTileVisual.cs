using UnityEngine;

[RequireComponent(typeof(FarmTile))]
public class FarmTileVisual : MonoBehaviour
{
    [Tooltip("未翻耕的土地贴图")]
    public Sprite unplowedSprite;
    [Tooltip("翻耕后的土地贴图")]
    public Sprite plowedSprite;
    [Tooltip("刚采摘完的预制件（小坑）")]
    public GameObject harvestedPitPrefab;
    [Tooltip("刚采摘完状态显示时间（秒）")]
    public float harvestedDisplayTime = 3f;
    [Tooltip("小坑位置偏移（相对于地块中心）")]
    public Vector2 pitOffset = new Vector2(0f, 0f);

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
            return;
        }
        spriteRenderer.sprite = farmTile.isPlowed ? plowedSprite : unplowedSprite;
    }

    private System.Collections.IEnumerator ShowHarvestedPit()
    {
        if (harvestedPitPrefab == null)
        {
            yield break;
        }

        isShowingHarvested = true;
        Vector3 pitPosition = transform.position + new Vector3(pitOffset.x, pitOffset.y, 0);

        // 实例化，不设父对象
        currentPit = Instantiate(harvestedPitPrefab, pitPosition, Quaternion.identity);

        // 验证 SpriteRenderer
        SpriteRenderer pitRenderer = currentPit.GetComponent<SpriteRenderer>();
        if (pitRenderer == null)
        {
            Debug.LogError($"FarmTileVisual ({gameObject.name}): HarvestedPit 缺少 SpriteRenderer！");
            Destroy(currentPit);
            yield break;
        }

        // 强制设置层级
        pitRenderer.sortingLayerName = "Crops";
        pitRenderer.sortingOrder = 1;

        yield return new WaitForSeconds(harvestedDisplayTime);

        if (currentPit != null)
        {
            Destroy(currentPit);
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
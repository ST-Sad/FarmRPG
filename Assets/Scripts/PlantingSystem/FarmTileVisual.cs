using UnityEngine;

[RequireComponent(typeof(FarmTile))]
public class FarmTileVisual : MonoBehaviour
{
    [Tooltip("δ������������ͼ")]
    public Sprite unplowedSprite;
    [Tooltip("�������������ͼ")]
    public Sprite plowedSprite;
    [Tooltip("�ղ�ժ���Ԥ�Ƽ���С�ӣ�")]
    public GameObject harvestedPitPrefab;
    [Tooltip("�ղ�ժ��״̬��ʾʱ�䣨�룩")]
    public float harvestedDisplayTime = 3f;
    [Tooltip("С��λ��ƫ�ƣ�����ڵؿ����ģ�")]
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

        // ʵ���������踸����
        currentPit = Instantiate(harvestedPitPrefab, pitPosition, Quaternion.identity);

        // ��֤ SpriteRenderer
        SpriteRenderer pitRenderer = currentPit.GetComponent<SpriteRenderer>();
        if (pitRenderer == null)
        {
            Debug.LogError($"FarmTileVisual ({gameObject.name}): HarvestedPit ȱ�� SpriteRenderer��");
            Destroy(currentPit);
            yield break;
        }

        // ǿ�����ò㼶
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
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MapPanel : abstractUIPanel
{
    [Header("UI组件")]
    [SerializeField] private Text currentMapText;
    [SerializeField] private Button returnButton;
    [SerializeField] private Transform mapButtonContainer;
    [SerializeField] private Button mapButtonPrefab;
    [SerializeField] private Image miniMapImage; // 新增：小地图 Image 引用
    [SerializeField] private Sprite myHomeMiniMap; // 新增：MyHome 小地图 Sprite
    [SerializeField] private Sprite outdoorMiniMap; // 新增：Outdoor 小地图 Sprite

    private void Start()
    {
        returnButton.onClick.AddListener(OnReturnClicked);
    }

    public override void Open()
    {
        base.Open();
        Refresh();
    }

    override public void Refresh()
    {
        // 清空现有按钮
        foreach (Transform child in mapButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // 显示当前地图名称
        var currentMap = MapManager.Instance.GetCurrentMapInfo();
        currentMapText.text = currentMap.mapName;

        // 设置返回按钮状态
        returnButton.interactable = !string.IsNullOrEmpty(currentMap?.parentMap);

        // 更新小地图显示
        UpdateMiniMap(currentMap.mapName);

        // 创建可传送的地图按钮
        List<string> availableMaps = MapManager.Instance.GetAvailableMaps();
        foreach (string mapName in availableMaps)
        {
            // 不显示当前地图
            if (mapName == MapManager.Instance.GetCurrentMapInfo()?.mapName)
                continue;
            Button mapButton = Instantiate(mapButtonPrefab, mapButtonContainer);
            mapButton.GetComponentInChildren<Text>().text = mapName;
            mapButton.onClick.AddListener(() => OnMapSelected(mapName));
        }
    }

    private void UpdateMiniMap(string mapName)
    {
        if (mapName == "MyHome")
        {
            miniMapImage.sprite = myHomeMiniMap;
        }
        else if (mapName == "Outdoor")
        {
            miniMapImage.sprite = outdoorMiniMap;
        }
    }

    private void OnReturnClicked()
    {
        MapManager.Instance.ReturnToParentMap();
        Close();
    }

    private void OnMapSelected(string mapName)
    {
        MapManager.Instance.SwitchToMap(mapName);
        Close();
    }
}
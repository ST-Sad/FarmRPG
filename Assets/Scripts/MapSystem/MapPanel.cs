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

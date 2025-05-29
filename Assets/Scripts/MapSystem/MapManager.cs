using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    [System.Serializable]
    public class MapInfo
    {
        public string mapName;          // 地图显示名称
        public string sceneName;        // 场景名称
        public Vector2 spawnPoint;      // 默认出生点
        public string parentMap;        // 上级地图名称（用于返回）
    }

    [Header("Map Settings")]
    [SerializeField] private List<MapInfo> mapDatabase = new List<MapInfo>();
    [SerializeField] private Transform player;
    private Dictionary<string, MapInfo> mapDictionary = new Dictionary<string, MapInfo>();
    private string currentMapName = "MyHome";
    private string previousMapName; // 用于记录上一个场景的名称

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
       
        mapDatabase.Add(new MapInfo
        {
            mapName = "Store",
            sceneName = "StoreScene", // 替换为你的 Store 场景的实际名称
            spawnPoint = new Vector2(-10, 1.8f), // 设置 Store 场景的默认出生点
            parentMap = "Outdoor"
        });

        foreach (var map in mapDatabase)
        {
            mapDictionary[map.mapName] = map;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SwitchToMap(string mapName)
    {
        if (!mapDictionary.ContainsKey(mapName))
        {
            Debug.LogError($"地图 {mapName} 未找到");
            return;
        }

        previousMapName = currentMapName; // 记录上一个场景的名称
        MapInfo targetMap = mapDictionary[mapName];
        SceneManager.LoadScene(targetMap.sceneName);
        currentMapName = mapName;
    }

    public void ReturnToParentMap()
    {
        if (mapDictionary.TryGetValue(currentMapName, out MapInfo currentMap))
        {
            if (!string.IsNullOrEmpty(currentMap.parentMap))
            {
                SwitchToMap(currentMap.parentMap);
            }
            else
            {
                Debug.LogWarning("当前地图没有设置上级地图");
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (mapDictionary.TryGetValue(currentMapName, out MapInfo mapInfo))
        {
            Vector2 spawnPoint = mapInfo.spawnPoint;

            // 根据上一个场景和当前场景设置不同的出生点
            if (currentMapName == "Outdoor")
            {
                if (previousMapName == "Home")
                {
                    spawnPoint = new Vector2(0, 0);
                }
                else if (previousMapName == "Store")
                {
                    spawnPoint = new Vector2(16, 11);
                }
            }
            else if (currentMapName == "Store" && previousMapName == "Outdoor")
            {
                spawnPoint = new Vector2(-10, 1.8f);
            }

            // 设置玩家出生点
            if (player != null)
            {
                player.position = spawnPoint;
            }
        }
    }

    public MapInfo GetCurrentMapInfo()
    {
        return mapDictionary.TryGetValue(currentMapName, out MapInfo info) ? info : null;
    }

    public List<string> GetAvailableMaps()
    {
        return new List<string>(mapDictionary.Keys);
    }
}
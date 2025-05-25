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
        public Vector3 spawnPoint;      // 默认出生点
        public string parentMap;        // 上级地图名称（用于返回）
    }

    [Header("Map Settings")]
    [SerializeField] private List<MapInfo> mapDatabase = new List<MapInfo>();
    [SerializeField] private Transform player;
    private Dictionary<string, MapInfo> mapDictionary = new Dictionary<string, MapInfo>();
    private string currentMapName;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
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
            // 设置玩家出生点
            if (player != null)
            {
                player.position = mapInfo.spawnPoint;
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

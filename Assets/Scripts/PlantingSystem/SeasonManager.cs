// SeasonManager.cs
using UnityEngine;

/// <summary>
/// 季节管理器，用于全局记录当前季节（可在编辑器中切换）
/// </summary>
public class SeasonManager : MonoBehaviour
{
    public enum Season { Spring, Summer, Autumn, Winter }
    [Tooltip("游戏开始时的季节")]
    public Season startingSeason = Season.Spring;
    public static Season currentSeason;

    void Awake()
    {
        // 将静态季节设置为起始季节
        currentSeason = startingSeason;
    }
}

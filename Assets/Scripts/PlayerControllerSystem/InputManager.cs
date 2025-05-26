// InputManager.cs
using UnityEngine;

/// <summary>
/// 管理玩家输入轴值，使用静态属性方便全局访问:contentReference[oaicite:16]{index=16}
/// </summary>
public class InputManager : MonoBehaviour
{
    public static float Horizontal { get; private set; }
    public static float Vertical { get; private set; }

    void Update()
    {
        // 获取水平和垂直轴输入（默认映射到 WASD/箭头等）
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
    }
}

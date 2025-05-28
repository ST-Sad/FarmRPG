// InventoryWindow.cs（修正后）
using UnityEngine;

public class InventoryWindow : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.I; // 切换按键（默认 I 键）
    private bool isOpen = true; // 背包实际状态（隐藏为默认）

    void Start()
    {
        // 初始化时根据 isOpen 状态设置 UI 显示（确保与脚本逻辑一致）
        gameObject.SetActive(isOpen);
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen; // 切换状态
        gameObject.SetActive(isOpen); // 同步更新 UI 显示
    }
}
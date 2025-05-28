using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryWindow : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.I; // 切换按键（默认 I 键）
    private bool isOpen = false; // 背包实际状态（显示为默认）
    private Canvas canvas; // Canvas 组件引用
    private InventoryUI inventoryUI; // InventoryUI 组件引用
    private InventoryManager inventoryManager; // 新增：InventoryManager 引用

    void Start()
    {
        // 获取 Canvas 组件
        canvas = GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = GetComponentInChildren<Canvas>(true);
            if (canvas == null)
            Debug.LogError("InventoryWindow: Canvas component not found on " + gameObject.name);
        }

        // 获取 InventoryUI 组件
        inventoryUI = GetComponentInChildren<InventoryUI>();
        if (inventoryUI == null)
        {
            Debug.LogWarning("InventoryWindow: InventoryUI component not found in children.");
        }

        // 获取 InventoryManager 组件
        inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryWindow: InventoryManager component not found in scene.");
        }
        else
        {
            // 订阅背包变化事件
            inventoryManager.OnInventoryChanged += () =>
            {
                if (inventoryUI != null && isOpen)
                {
                    inventoryUI.RefreshUI();
                    Debug.Log("InventoryWindow: InventoryUI refreshed due to inventory change.");
                }
            };
        }

        // 确保 Canvas 初始状态与 isOpen 一致
        gameObject.SetActive(isOpen);
        if (canvas != null)
        {
            canvas.enabled = isOpen;
        }

        // 确保 EventSystem 存在
        if (FindObjectOfType<EventSystem>() == null)
        {
            Debug.LogWarning("InventoryWindow: No EventSystem found in scene. Creating one.");
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        // 初始刷新 UI
        if (isOpen && inventoryUI != null)
        {
            inventoryUI.RefreshUI();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            Debug.Log("InventoryWindow: Toggle key pressed. Current isOpen: " + isOpen);
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen; // 切换状态
        Debug.Log("InventoryWindow: Toggling inventory. New isOpen: " + isOpen);

        // 设置 GameObject 的激活状态
        gameObject.SetActive(isOpen);

        // 额外确保 Canvas 组件启用
        if (canvas != null)
        {
            canvas.enabled = isOpen;
        }
        else
        {
            Debug.LogWarning("InventoryWindow: Canvas component is null during toggle.");
        }

        // 如果打开背包，强制刷新 UI
        if (isOpen && inventoryUI != null)
        {
            inventoryUI.RefreshUI();
            Debug.Log("InventoryWindow: InventoryUI refreshed.");
        }
    }
}
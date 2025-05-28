using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryWindow : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.I; // �л�������Ĭ�� I ����
    private bool isOpen = false; // ����ʵ��״̬����ʾΪĬ�ϣ�
    private Canvas canvas; // Canvas �������
    private InventoryUI inventoryUI; // InventoryUI �������
    private InventoryManager inventoryManager; // ������InventoryManager ����

    void Start()
    {
        // ��ȡ Canvas ���
        canvas = GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = GetComponentInChildren<Canvas>(true);
            if (canvas == null)
            Debug.LogError("InventoryWindow: Canvas component not found on " + gameObject.name);
        }

        // ��ȡ InventoryUI ���
        inventoryUI = GetComponentInChildren<InventoryUI>();
        if (inventoryUI == null)
        {
            Debug.LogWarning("InventoryWindow: InventoryUI component not found in children.");
        }

        // ��ȡ InventoryManager ���
        inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryWindow: InventoryManager component not found in scene.");
        }
        else
        {
            // ���ı����仯�¼�
            inventoryManager.OnInventoryChanged += () =>
            {
                if (inventoryUI != null && isOpen)
                {
                    inventoryUI.RefreshUI();
                    Debug.Log("InventoryWindow: InventoryUI refreshed due to inventory change.");
                }
            };
        }

        // ȷ�� Canvas ��ʼ״̬�� isOpen һ��
        gameObject.SetActive(isOpen);
        if (canvas != null)
        {
            canvas.enabled = isOpen;
        }

        // ȷ�� EventSystem ����
        if (FindObjectOfType<EventSystem>() == null)
        {
            Debug.LogWarning("InventoryWindow: No EventSystem found in scene. Creating one.");
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        // ��ʼˢ�� UI
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
        isOpen = !isOpen; // �л�״̬
        Debug.Log("InventoryWindow: Toggling inventory. New isOpen: " + isOpen);

        // ���� GameObject �ļ���״̬
        gameObject.SetActive(isOpen);

        // ����ȷ�� Canvas �������
        if (canvas != null)
        {
            canvas.enabled = isOpen;
        }
        else
        {
            Debug.LogWarning("InventoryWindow: Canvas component is null during toggle.");
        }

        // ����򿪱�����ǿ��ˢ�� UI
        if (isOpen && inventoryUI != null)
        {
            inventoryUI.RefreshUI();
            Debug.Log("InventoryWindow: InventoryUI refreshed.");
        }
    }
}
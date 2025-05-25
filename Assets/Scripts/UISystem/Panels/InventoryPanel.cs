using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : abstractUIPanel
{
    [Header("Inventory Settings")]
    [SerializeField] private Transform itemsContainer;//物品容器
    [SerializeField] private GameObject itemSlotPrefab;//物品槽预制体
    [SerializeField] private Button closeButton;//关闭按钮
    protected override void Awake()
    {
        closeButton.onClick.AddListener(() => UIManager.Instance.ClosePanel(PanelName));
    }
    public override void Refresh()
    {
        InventoryUI inventoryUI = GetComponent<InventoryUI>(); 
        inventoryUI.RefreshUI();
    }
}

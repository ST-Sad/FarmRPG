using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventHeader : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField] private KeyCode openMenu = KeyCode.Escape; // 打开菜单的按键
    [SerializeField] private KeyCode openQuestLog = KeyCode.L;// 打开任务日志的按键
    [SerializeField] private KeyCode openMapKey = KeyCode.M;
    [SerializeField] private KeyCode openInventoryKey = KeyCode.I;

    void Start()
    {
       DontDestroyOnLoad(this.gameObject); 
    }
    void Update()
    {
         if (Input.GetKeyDown(openMapKey))
        {
            if (UIManager.Instance.IsPanelOpen("Map"))
            {
                UIManager.Instance.ClosePanel("Map");
            }
            else
            {
                UIManager.Instance.ShowPanel("Map");
            }
        }
        if(Input.GetKeyDown(openInventoryKey)){
            if (UIManager.Instance.IsPanelOpen("Inventory"))
            {
                UIManager.Instance.ClosePanel("Inventory");
            }
            else
            {
                UIManager.Instance.ShowPanel("Inventory");
            }
        }
        if (Input.GetKeyDown(openQuestLog))
        {
            if (UIManager.Instance.IsPanelOpen("Quest"))//如果任务日志面板已经打开，则关闭
            {
                UIManager.Instance.ClosePanel("Quest");//关闭任务日志面板
            }
            else
            {
                UIManager.Instance.ShowPanel("Quest");//打开任务日志面板
            }
        }
        if (Input.GetKeyDown(openMenu))
            {
                Debug.Log("被按下");
                if (UIManager.Instance.IsPanelOpen("Menu"))//如果菜单面板已经打开，则关闭
                {
                    UIManager.Instance.ClosePanel("Menu");//关闭菜单面板
                }
                else if (UIManager.Instance.IsPanelOpen("Quest"))//如果任务日志面板已经打开，则关闭
                {
                    UIManager.Instance.ClosePanel("Quest");//关闭任务日志面板
                }
                else if (UIManager.Instance.IsPanelOpen("Map"))//如果地图面板已经打开，则关闭
                {
                    UIManager.Instance.ClosePanel("Map");//关闭地图面板
                }
                else if (UIManager.Instance.IsPanelOpen("Inventory"))//如果背包面板已经打开，则关闭
                {
                    UIManager.Instance.ClosePanel("Inventory");//关闭背包面板
                }
                else if (UIManager.Instance.IsPanelOpen("Dialogue"))//如果对话面板已经打开，则关闭
                {
                    UIManager.Instance.ClosePanel("Dialogue");//关闭对话面板
                }
                else
                {
                    UIManager.Instance.ShowPanel("Menu");//打开菜单面板
                    Debug.Log("打开");
                }
            }
    }
}

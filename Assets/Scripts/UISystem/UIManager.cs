using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [System.Serializable]
    public class PanelInfo
    {
        public string panelName;//面板名称
        public abstractUIPanel panel;//面板对象
        public bool startHidden = true;//是否隐藏
        public int priority = 0;//优先级
    }
    [Header("UI Settings")]
    [SerializeField] private Transform uiParent;
    [SerializeField]
    private List<PanelInfo> panelInfos = new List<PanelInfo>();
    private Dictionary<string, abstractUIPanel> panels = new Dictionary<string, abstractUIPanel>();//面板对象集合
    private Stack<string> panelHistory = new Stack<string>();
    private void Awake()
    {
        Debug.Log("awake");
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        InitilizePanels();
        
    }
    private void InitilizePanels()
    {
        foreach (var info in panelInfos)
        {
            if (info.panel != null)
            {
                panels[info.panelName] = info.panel;
                if (info.startHidden)
                {
                    info.panel.gameObject.SetActive(false);
                    Debug.Log("隐藏"+info.panelName);
                }
            }
        }
    }
    public void ShowPanel(string panelName)
    {
        Debug.Log("show"+panelName);
        if (!panels.ContainsKey(panelName)) return;

        if (panelHistory.Count > 0)
        {
            var currentPanel = panels[panelHistory.Peek()];
            currentPanel.Close();
        }
        //显示面板
        Debug.Log("show2"+panels[panelName].gameObject.name);
        panels[panelName].Open();
        panelHistory.Push(panelName);
    }
    public void ClosePanel(string panelName)
    {
        if (!panels.ContainsKey(panelName)) return;
        Debug.Log("close" + panelName);
        panels[panelName].Close();
        if (panelHistory.Count > 0 && panelHistory.Peek() == panelName)
        {
            panelHistory.Pop();
        }
    }
    public void Back()
    {
        if (panelHistory.Count <= 1) return;

        // 关闭当前面板
        var currentPanelName = panelHistory.Pop();
        panels[currentPanelName].Close();

        // 打开上一个面板
        var previousPanelName = panelHistory.Peek();
        panels[previousPanelName].Open();
    }
    public bool IsPanelOpen(string panelName)
    {
        // 检查字典中是否存在该面板
        if (panels.TryGetValue(panelName, out abstractUIPanel panel))
        {
            return panel != null && panel.IsOpen;
        }
        return false;
    }
}



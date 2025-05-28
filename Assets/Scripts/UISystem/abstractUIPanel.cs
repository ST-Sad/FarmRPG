using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class abstractUIPanel : MonoBehaviour
{
    [Header("Panel Settings")]
    [SerializeField] private string panelName;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] protected UnityEvent onPanelOpened;
    [SerializeField] protected UnityEvent onPanelClosed;
    public string PanelName => panelName;
    public bool IsOpen { get; private set; }
    protected virtual void Awake()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        DontDestroyOnLoad(gameObject);
    }
    public virtual void Open()
    {
        Debug.Log("抽象类被调用");
        gameObject.SetActive(true);
        IsOpen = true;
        canvasGroup.alpha = 1f;
        OnOpened();
    }
    public virtual void Close()
    {
        if (!IsOpen) return;
        Debug.Log("关闭"+panelName);
        canvasGroup.alpha = 0f;
        OnClosed();
        Canvas.ForceUpdateCanvases();
        gameObject.SetActive(false);
    }
    protected virtual void OnOpened()
    {
        onPanelOpened.Invoke();
        Refresh();
    }
    protected virtual void OnClosed()
    {
        onPanelClosed.Invoke();
        IsOpen = false;
    }

    public abstract void Refresh();
}

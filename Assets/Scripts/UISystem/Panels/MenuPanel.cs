using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuPanel : abstractUIPanel
{

    [Header("主菜单设置")]
    [SerializeField] private Button saveButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    public override void Refresh()
    { }
    protected void Awake()
    {
        Debug.Log("被打开");
        base.Awake();
        settingsButton.onClick.AddListener(OnSettings);
        quitButton.onClick.AddListener(OnQuit);
    }
    private void OnSettings()
    {
        UIManager.Instance.ShowPanel("Settings");
    }
     private void OnQuit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsPanel : abstractUIPanel
{
    [Header("Settings References")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Button backButton;
    [SerializeField] private AudioSource bgmAudioSource;

    protected void Awake()
    {
        base.Awake();
        backButton.onClick.AddListener(() => UIManager.Instance.Back());
    }

    public override void Refresh()
    {
        // 从AudioManager加载当前音量设置
    }
    /*
        public void OnMasterVolumeChanged(float value)
        {

        }*/

    public void OnMusicVolumeChanged(float value)
    {
        bgmAudioSource.volume = value;
    }
}

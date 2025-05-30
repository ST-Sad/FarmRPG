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

    protected void Awake()
    {
        base.Awake();
        backButton.onClick.AddListener(() => UIManager.Instance.Back());
    }

    public override void Refresh()
    {
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.value = AudioManager.Instance.sfxPool[0].volume;
        }
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = AudioManager.Instance.bgmSource.volume;
        }
    }

    public void OnMasterVolumeChanged(float value)
    {
        foreach (var audioSource in AudioManager.Instance.sfxPool)
        {
            audioSource.volume = value;
        }
    }

    public void OnMusicVolumeChanged(float value)
    {
        AudioManager.Instance.bgmSource.volume = value;
    }
}

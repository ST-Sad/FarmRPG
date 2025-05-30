using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance{get; private set;}

    [Header("BGM")]
    public AudioSource bgmSource;
    public AudioClip defaultBGM;

    [Header("SFX Pool")]
    public int initialPoolSize = 5; // 初始AudioSource数量
    public List<AudioSource> sfxPool = new List<AudioSource>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 初始化BGM
        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true;
        }

        // 初始化SFX对象池
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewAudioSource();
        }
    }

    // 创建新的AudioSource并加入池
    private AudioSource CreateNewAudioSource()
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.playOnAwake = false;
        sfxPool.Add(newSource);
        return newSource;
    }

    // 从池中获取可用的AudioSource
    private AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource source in sfxPool)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }

        // 如果所有AudioSource都在使用，就新建一个
        return CreateNewAudioSource();
    }

    // 播放音效（动态分配AudioSource）
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        AudioSource source = GetAvailableAudioSource();
        source.clip = clip;
        source.Play();
    }
}
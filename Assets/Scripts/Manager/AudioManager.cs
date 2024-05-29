using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 音频管理器类，用于管理和播放音频。
 */
public class AudioManager : MonoBehaviour
{

    // 音频管理器的单例实例
    public static AudioManager Instance { get; private set; }

    // 用于播放音频的AudioSource组件
    private AudioSource audioSource;

    /**
     * Awake函数，在对象唤醒时调用。
     * 设置单例实例并初始化AudioSource。
     */
    private void Awake()
    {
        Instance = this; // 设置单例实例
        audioSource = GetComponent<AudioSource>(); // 获取并初始化AudioSource组件
    }

    // 在对象开始时调用的函数
    private void Start()
    {
        //PlayBgm(Config.bgm1); // 示例代码，启动时播放背景音乐
    }

    /**
     * 播放背景音乐。
     * @param path 音频资源的路径。
     */
    public void PlayBgm(string path)
    {
        AudioClip ac = Resources.Load<AudioClip>(path); // 加载音频剪辑
        audioSource.clip = ac; // 设置音频源的音频剪辑
        audioSource.Play(); // 播放音频
    }
    
    /**
     * 播放一次性音效。
     * @param path 音频资源的路径。
     * @param volume 音量大小，默认为1。
     */
    public void PlayClip(string path,float volume=1)
    {
        AudioClip ac = Resources.Load<AudioClip>(path); // 加载音频剪辑
        AudioSource.PlayClipAtPoint(ac, transform.position, volume); // 在当前位置播放音效
    }

}

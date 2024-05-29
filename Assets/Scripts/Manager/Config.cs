using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

/**
 * 配置类：用于存储游戏中的各种配置信息，例如音频资源路径。
 */
public class Config 
{
    // 背景音乐1的资源路径
    public const string bgm1 = "Audio/Music/bgm1";
    // 点击按钮时的音效资源路径
    public const string btn_click = "Audio/Sound/buttonclick";
    // 吃东西时的音效资源路径
    public const string eat = "Audio/Sound/chompsoft";
    // 最后一波浪的音效资源路径
    public const string lastwave = "Audio/Sound/hugewave";
    // 失败时的背景音乐资源路径
    public const string lose_music = "Audio/Sound/losemusic";
    // 胜利时的背景音乐资源路径
    public const string win_music = "Audio/Sound/winmusic";
    // 植物生长的音效资源路径
    public const string plant = "Audio/Sound/plant";
    // 射击的音效资源路径
    public const string shoot = "Audio/Sound/shoot";
}


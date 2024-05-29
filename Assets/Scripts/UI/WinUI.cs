using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/**
 * WinUI类负责管理游戏胜利时的用户界面。
 * 它继承自MonoBehaviour，利用Animator来控制UI的显示和隐藏。
 */
public class WinUI : MonoBehaviour
{
    private Animator anim; // 控制UI动画的Animator组件

    /**
     * Awake在对象激活时被调用。
     * 它初始化了WinUI类的Animator组件。
     */
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /**
     * Start在对象开始运行时被调用。
     * 它在游戏开始时隐藏胜利UI。
     */
    private void Start()
    {
        Hide();
    }

    /**
     * 隐藏胜利UI。
     * 它通过禁用Animator组件来实现UI的隐藏。
     */
    void Hide()
    {
        anim.enabled = false;
    }

    /**
     * 显示胜利UI。
     * 它通过启用Animator组件来实现UI的显示。
     */
    public void Show()
    {
        anim.enabled = true;
    }

}

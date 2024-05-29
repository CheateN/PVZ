using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * FailUI类用于管理游戏失败时的用户界面（UI）动画。
 */
public class FailUI : MonoBehaviour
{

    private Animator anim; // Animator组件，用于控制UI的动画。

    /**
     * Awake函数在对象初始化时调用，用于获取Animator组件。
     */
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /**
     * Start函数在对象开始运行时调用，用于初始化时隐藏失败UI。
     */
    private void Start()
    {
        Hide();
    }

    /**
     * 隐藏UI的方法。
     * 通过禁用Animator组件来停止UI的动画。
     */
    void Hide() 
    {
        anim.enabled = false;
    }

    /**
     * 显示UI的方法。
     * 通过启用Animator组件来恢复UI的动画。
     */
    public void Show()
    {
        anim.enabled = true;
    }

}

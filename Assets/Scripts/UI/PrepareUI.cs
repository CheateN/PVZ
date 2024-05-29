using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * PrepareUI类负责UI的准备和显示工作。
 */
public class PrepareUI : MonoBehaviour
{
    private Animator anim; // Animator组件，用于控制UI的动画。

    private Action onComplete; // 完成显示后需要执行的动作。

    private void Start()
    {
        anim = GetComponent<Animator>(); // 获取Animator组件。
        anim.enabled = false; // 初始时禁用动画。
    }

    /**
     * 显示UI，并在显示完成后执行指定的动作。
     * @param onComplete 显示完成后要执行的动作。
     */
    public void Show(Action onComplete)
    {
        this.onComplete = onComplete; // 保存完成后的动作。
        anim.enabled = true; // 启用动画，开始显示UI。
    }

    /**
     * 显示完成后调用此方法，执行传入的完成动作。
     */
    void OnShowComplete()
    {
        onComplete?.Invoke(); // 调用完成动作。
    }

}

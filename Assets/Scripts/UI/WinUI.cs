using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/**
 * WinUI�ฺ�������Ϸʤ��ʱ���û����档
 * ���̳���MonoBehaviour������Animator������UI����ʾ�����ء�
 */
public class WinUI : MonoBehaviour
{
    private Animator anim; // ����UI������Animator���

    /**
     * Awake�ڶ��󼤻�ʱ�����á�
     * ����ʼ����WinUI���Animator�����
     */
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /**
     * Start�ڶ���ʼ����ʱ�����á�
     * ������Ϸ��ʼʱ����ʤ��UI��
     */
    private void Start()
    {
        Hide();
    }

    /**
     * ����ʤ��UI��
     * ��ͨ������Animator�����ʵ��UI�����ء�
     */
    void Hide()
    {
        anim.enabled = false;
    }

    /**
     * ��ʾʤ��UI��
     * ��ͨ������Animator�����ʵ��UI����ʾ��
     */
    public void Show()
    {
        anim.enabled = true;
    }

}

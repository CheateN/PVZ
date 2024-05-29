using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * PrepareUI�ฺ��UI��׼������ʾ������
 */
public class PrepareUI : MonoBehaviour
{
    private Animator anim; // Animator��������ڿ���UI�Ķ�����

    private Action onComplete; // �����ʾ����Ҫִ�еĶ�����

    private void Start()
    {
        anim = GetComponent<Animator>(); // ��ȡAnimator�����
        anim.enabled = false; // ��ʼʱ���ö�����
    }

    /**
     * ��ʾUI��������ʾ��ɺ�ִ��ָ���Ķ�����
     * @param onComplete ��ʾ��ɺ�Ҫִ�еĶ�����
     */
    public void Show(Action onComplete)
    {
        this.onComplete = onComplete; // ������ɺ�Ķ�����
        anim.enabled = true; // ���ö�������ʼ��ʾUI��
    }

    /**
     * ��ʾ��ɺ���ô˷�����ִ�д������ɶ�����
     */
    void OnShowComplete()
    {
        onComplete?.Invoke(); // ������ɶ�����
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * FailUI�����ڹ�����Ϸʧ��ʱ���û����棨UI��������
 */
public class FailUI : MonoBehaviour
{

    private Animator anim; // Animator��������ڿ���UI�Ķ�����

    /**
     * Awake�����ڶ����ʼ��ʱ���ã����ڻ�ȡAnimator�����
     */
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /**
     * Start�����ڶ���ʼ����ʱ���ã����ڳ�ʼ��ʱ����ʧ��UI��
     */
    private void Start()
    {
        Hide();
    }

    /**
     * ����UI�ķ�����
     * ͨ������Animator�����ֹͣUI�Ķ�����
     */
    void Hide() 
    {
        anim.enabled = false;
    }

    /**
     * ��ʾUI�ķ�����
     * ͨ������Animator������ָ�UI�Ķ�����
     */
    public void Show()
    {
        anim.enabled = true;
    }

}

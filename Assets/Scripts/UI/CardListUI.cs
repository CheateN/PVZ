using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/**
 * CardListUI�ฺ��������б��UI��ʾ��
 */
public class CardListUI : MonoBehaviour
{
    public List<Card> cardList; // �洢���ƶ�����б�

    private void Start()
    {
        DisableCardList(); // �ڿ�ʼʱ���ÿ����б�

        //ShowCardList();
    }

    /**
     * ��ʾ�����б�
     * ʹ��DOTween����ƽ���ص���UIλ������ʾ�����б�
     */
    public void ShowCardList()
    {
        GetComponent<RectTransform>().DOAnchorPosY(-48,1); // ��������UIλ��
        EnableCardList(); // ���ÿ����б�
    }
    
    /**
     * ���ÿ����б�
     * ���������б�����ÿ�ſ��ơ�
     */
    public void DisableCardList()
    {
        foreach(Card card in cardList)
        {
            card.DisableCard(); // ����ÿ�ſ���
        }
    }
    
    /**
     * ���ÿ����б�
     * ���������б�����ÿ�ſ��ơ�
     */
    void EnableCardList()
    {
        foreach (Card card in cardList)
        {
            card.EnableCard(); // ����ÿ�ſ���
        }
    }
    
}

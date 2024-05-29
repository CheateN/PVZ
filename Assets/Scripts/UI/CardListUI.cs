using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/**
 * CardListUI类负责管理卡牌列表的UI显示。
 */
public class CardListUI : MonoBehaviour
{
    public List<Card> cardList; // 存储卡牌对象的列表

    private void Start()
    {
        DisableCardList(); // 在开始时禁用卡牌列表

        //ShowCardList();
    }

    /**
     * 显示卡牌列表。
     * 使用DOTween动画平滑地调整UI位置以显示卡牌列表。
     */
    public void ShowCardList()
    {
        GetComponent<RectTransform>().DOAnchorPosY(-48,1); // 动画调整UI位置
        EnableCardList(); // 启用卡牌列表
    }
    
    /**
     * 禁用卡牌列表。
     * 遍历卡牌列表，禁用每张卡牌。
     */
    public void DisableCardList()
    {
        foreach(Card card in cardList)
        {
            card.DisableCard(); // 禁用每张卡牌
        }
    }
    
    /**
     * 启用卡牌列表。
     * 遍历卡牌列表，启用每张卡牌。
     */
    void EnableCardList()
    {
        foreach (Card card in cardList)
        {
            card.EnableCard(); // 启用每张卡牌
        }
    }
    
}

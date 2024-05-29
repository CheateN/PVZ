using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 卡牌状态枚举，定义了卡片的不同状态
enum CardState
{
    Disable, // 不可用
    Cooling, // 冷却中
    WaitingSun, // 等待阳光
    Ready // 准备就绪
}

// 植物类型枚举
public enum PlantType
{
    Sunflower, // 向日葵
    PeaShooter // 豌豆射手
}

// 卡片类，负责管理植物卡片的行为和状态
public class Card : MonoBehaviour
{
    // 卡片当前状态
    private CardState cardState = CardState.Disable;
    // 卡片代表的植物类型
    public PlantType plantType = PlantType.Sunflower;

    // 卡片的亮光效果对象
    public GameObject cardLight;
    // 卡片的灰色效果对象，用于表示不可用状态
    public GameObject cardGray;
    // 卡片的冷却遮罩图像
    public Image cardMask;

    // 卡片冷却时间
    [SerializeField]
    private float cdTime = 2;
    // 当前冷却计时器
    private float cdTimer = 0;

    // 卡片需要的阳光点数
    [SerializeField]
    private int needSunPoint = 50;

    // 每帧更新，根据当前卡片状态执行不同逻辑
    private void Update()
    {
        switch (cardState)
        {
            case CardState.Cooling:
                CoolingUpdate(); // 冷却状态更新
                break;
            case CardState.WaitingSun:
                WaitingSunUpdate(); // 等待阳光状态更新
                break;
            case CardState.Ready:
                ReadyUpdate(); // 准备就绪状态更新
                break;
            default:
                break;
        }
    }

    // 冷却状态更新逻辑
    void CoolingUpdate()
    {
        cdTimer += Time.deltaTime;

        cardMask.fillAmount = (cdTime - cdTimer) / cdTime;

        if (cdTimer >= cdTime)
        {
            TransitionToWaitingSun(); // 切换到等待阳光状态
        }

    }
    // 等待阳光状态更新逻辑
    void WaitingSunUpdate()
    {
        if (needSunPoint <= SunManager.Instance.SunPoint)
        {
            TransitionToReady(); // 切换到准备就绪状态
        }
    }
    // 准备就绪状态更新逻辑
    void ReadyUpdate()
    {
        if (needSunPoint > SunManager.Instance.SunPoint)
        {
            TransitionToWaitingSun(); // 切换到等待阳光状态
        }

    }

    // 切换到等待阳光状态
    void TransitionToWaitingSun()
    {
        cardState = CardState.WaitingSun;

        cardLight.SetActive(false);
        cardGray.SetActive(true);
        cardMask.gameObject.SetActive(false);
    }
    // 切换到准备就绪状态
    void TransitionToReady()
    {
        cardState = CardState.Ready;

        cardLight.SetActive(true);
        cardGray.SetActive(false);
        cardMask.gameObject.SetActive(false);
    }
    // 切换到冷却状态
    void TransitionToCooling()
    {
        cardState = CardState.Cooling;
        cdTimer = 0;
        cardLight.SetActive(false);
        cardGray.SetActive(true);
        cardMask.gameObject.SetActive(true);
    }

    // 卡片点击事件处理
    public void OnClick()
    {
        AudioManager.Instance.PlayClip(Config.btn_click); // 播放点击音效
        if (cardState == CardState.Disable) return; // 如果卡片状态为不可用，则直接返回
        if (needSunPoint > SunManager.Instance.SunPoint) return; // 如果需要的阳光点数大于当前阳光点数，则直接返回

        
        bool isSuccess = HandManager.Instance.AddPlant(plantType); // 尝试在手牌中添加植物
        if (isSuccess)
        {
            SunManager.Instance.SubSun(needSunPoint); // 如果添加成功，则减少相应数量的阳光
            TransitionToCooling(); // 进入冷却状态
        }
    }

    // 将卡片设置为不可用状态
    public void DisableCard()
    {
        cardState = CardState.Disable;
    }
    // 将卡片设置为准备就绪状态（从不可用状态直接变为冷却状态）
    public void EnableCard()
    {
        TransitionToCooling();
    }

}

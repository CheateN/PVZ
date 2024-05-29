using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ����״̬ö�٣������˿�Ƭ�Ĳ�ͬ״̬
enum CardState
{
    Disable, // ������
    Cooling, // ��ȴ��
    WaitingSun, // �ȴ�����
    Ready // ׼������
}

// ֲ������ö��
public enum PlantType
{
    Sunflower, // ���տ�
    PeaShooter // �㶹����
}

// ��Ƭ�࣬�������ֲ�￨Ƭ����Ϊ��״̬
public class Card : MonoBehaviour
{
    // ��Ƭ��ǰ״̬
    private CardState cardState = CardState.Disable;
    // ��Ƭ�����ֲ������
    public PlantType plantType = PlantType.Sunflower;

    // ��Ƭ������Ч������
    public GameObject cardLight;
    // ��Ƭ�Ļ�ɫЧ���������ڱ�ʾ������״̬
    public GameObject cardGray;
    // ��Ƭ����ȴ����ͼ��
    public Image cardMask;

    // ��Ƭ��ȴʱ��
    [SerializeField]
    private float cdTime = 2;
    // ��ǰ��ȴ��ʱ��
    private float cdTimer = 0;

    // ��Ƭ��Ҫ���������
    [SerializeField]
    private int needSunPoint = 50;

    // ÿ֡���£����ݵ�ǰ��Ƭ״ִ̬�в�ͬ�߼�
    private void Update()
    {
        switch (cardState)
        {
            case CardState.Cooling:
                CoolingUpdate(); // ��ȴ״̬����
                break;
            case CardState.WaitingSun:
                WaitingSunUpdate(); // �ȴ�����״̬����
                break;
            case CardState.Ready:
                ReadyUpdate(); // ׼������״̬����
                break;
            default:
                break;
        }
    }

    // ��ȴ״̬�����߼�
    void CoolingUpdate()
    {
        cdTimer += Time.deltaTime;

        cardMask.fillAmount = (cdTime - cdTimer) / cdTime;

        if (cdTimer >= cdTime)
        {
            TransitionToWaitingSun(); // �л����ȴ�����״̬
        }

    }
    // �ȴ�����״̬�����߼�
    void WaitingSunUpdate()
    {
        if (needSunPoint <= SunManager.Instance.SunPoint)
        {
            TransitionToReady(); // �л���׼������״̬
        }
    }
    // ׼������״̬�����߼�
    void ReadyUpdate()
    {
        if (needSunPoint > SunManager.Instance.SunPoint)
        {
            TransitionToWaitingSun(); // �л����ȴ�����״̬
        }

    }

    // �л����ȴ�����״̬
    void TransitionToWaitingSun()
    {
        cardState = CardState.WaitingSun;

        cardLight.SetActive(false);
        cardGray.SetActive(true);
        cardMask.gameObject.SetActive(false);
    }
    // �л���׼������״̬
    void TransitionToReady()
    {
        cardState = CardState.Ready;

        cardLight.SetActive(true);
        cardGray.SetActive(false);
        cardMask.gameObject.SetActive(false);
    }
    // �л�����ȴ״̬
    void TransitionToCooling()
    {
        cardState = CardState.Cooling;
        cdTimer = 0;
        cardLight.SetActive(false);
        cardGray.SetActive(true);
        cardMask.gameObject.SetActive(true);
    }

    // ��Ƭ����¼�����
    public void OnClick()
    {
        AudioManager.Instance.PlayClip(Config.btn_click); // ���ŵ����Ч
        if (cardState == CardState.Disable) return; // �����Ƭ״̬Ϊ�����ã���ֱ�ӷ���
        if (needSunPoint > SunManager.Instance.SunPoint) return; // �����Ҫ������������ڵ�ǰ�����������ֱ�ӷ���

        
        bool isSuccess = HandManager.Instance.AddPlant(plantType); // ���������������ֲ��
        if (isSuccess)
        {
            SunManager.Instance.SubSun(needSunPoint); // �����ӳɹ����������Ӧ����������
            TransitionToCooling(); // ������ȴ״̬
        }
    }

    // ����Ƭ����Ϊ������״̬
    public void DisableCard()
    {
        cardState = CardState.Disable;
    }
    // ����Ƭ����Ϊ׼������״̬���Ӳ�����״ֱ̬�ӱ�Ϊ��ȴ״̬��
    public void EnableCard()
    {
        TransitionToCooling();
    }

}

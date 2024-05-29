using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ֲ��״̬ö�٣�����ֲ�������״̬�����ú�����
enum PlantState
{
    Disable, // ����״̬
    Enable  // ����״̬
}

// ֲ���࣬�������ֲ��Ļ�����Ϊ��״̬
public class Plant : MonoBehaviour
{
    // ֲ��ĵ�ǰ״̬��Ĭ��Ϊ����
    PlantState plantState = PlantState.Disable;
    // ֲ������ͣ�Ĭ��Ϊ���տ�
    public PlantType plantType = PlantType.Sunflower;

    // ֲ��Ľ���ֵ��Ĭ��Ϊ100
    public int HP = 100;

    // ��ֲ������ʼ��ʱ���ã���ֲ��״̬����Ϊ���ã������ö�������ײ��
    private void Start()
    {
        TransitionToDisable();
    }

    // ÿ֡����ʱ����ֲ�ﵱǰ״ִ̬�в�ͬ�ĸ����߼�
    private void Update()
    {
        switch (plantState)
        {
            case PlantState.Disable:
                DisableUpdate(); // �������״̬�µĸ���
                break;
            case PlantState.Enable:
                EnableUpdate(); // ��������״̬�µĸ���
                break;
            default:
                break;
        }
    }

    // ����״̬�µĸ����߼�����ǰΪ��ʵ�֣��������ͨ�����Ǵ˷������ṩ�����߼�
    void DisableUpdate()
    {

    }
    // ����״̬�µĸ����߼�����ǰΪ��ʵ�֣��������ͨ�����Ǵ˷������ṩ�����߼�
    protected virtual void EnableUpdate()
    {

    }

    // ��ֲ��״̬ת�Ƶ����ã������ö��������2D��ײ��
    void TransitionToDisable()
    {
        plantState = PlantState.Disable;
        GetComponent<Animator>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    // ��ֲ��״̬ת�Ƶ����ã������ö��������2D��ײ��
    public void TransitionToEnable()
    {
        plantState = PlantState.Enable;
        GetComponent<Animator>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    // ��ֲ������˺���������ֵ����0�����£������Die��������ֲ�����
    public void TakeDamage(int damage)
    {
        this.HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    // ����ֲ����Ϸ����ʵ��ֲ�������߼�
    private void Die()
    {
        Destroy(gameObject);
    }
}


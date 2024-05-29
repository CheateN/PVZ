using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * ��ʾһ����Ԫ����࣬������ֲֲ�
 */
public class Cell : MonoBehaviour
{
    // ��ǰ��Ԫ������ֲ��ֲ��
    public Plant currentPlant;

    /**
     * ���������Ԫ��ʱ���á�
     * ������¼����ݸ��ֲ���������
     */
    private void OnMouseDown()
    {
        HandManager.Instance.OnCellClick(this);
    }

    /**
     * �����ڵ�Ԫ�������һ��ֲ�
     * @param plant Ҫ��ӵ�ֲ�����
     * @return ����ɹ����ֲ�ﷵ��true�������Ԫ��������ֲ���򷵻�false��
     */
    public bool AddPlant(Plant plant)
    {
        // �����ǰ��Ԫ������ֲ������
        if (currentPlant != null) return false;

        currentPlant = plant;
        currentPlant.transform.position = transform.position; // ����ֲ��λ���뵥Ԫ����ͬ
        plant.TransitionToEnable(); // ����ֲ��
        return true;
    }

}

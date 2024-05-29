using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/**
 * ��ʾ̫�����࣬����̫�����ƶ������ͽ����߼���
 */
public class Sun : MonoBehaviour
{
    // ̫���ƶ���Ŀ��λ�������ʱ��
    public float moveDuration = 1;
    // ̫�����ռ�ʱ���ӵķ���
    public int point = 50;

    /**
     * ʹ�������˶��ķ�ʽ����̫���ƶ���ָ����λ�á�
     * @param targetPos Ŀ��λ�õ�����3D���ꡣ
     */
    public void LinearTo(Vector3 targetPos)
    {
        transform.DOMove(targetPos, moveDuration); // ֱ���ƶ���Ŀ��λ��
    }
    
    /**
     * ʹ�������˶��ķ�ʽ����̫������ת����ָ����λ�á�
     * @param targetPos Ŀ��λ�õ�����3D���ꡣ
     */
    public void JumpTo(Vector3 targetPos)
    {
        targetPos.z = -1; // ����Ŀ��λ�õ�z������Ϊ-1
        Vector3 centerPos = (transform.position + targetPos) / 2; // �����м�λ��
        float distance = Vector3.Distance(transform.position, targetPos); // ���㵱ǰλ�õ�Ŀ��λ�õľ���

        centerPos.y += (distance/2); // ���ݾ�������м�λ�õ�y������

        // ʹ��Catmull-Rom���ߴӵ�ǰλ�þ����м�λ���ƶ���Ŀ��λ��
        transform.DOPath(new Vector3[] { transform.position, centerPos, targetPos },
            moveDuration, PathType.CatmullRom).SetEase(Ease.OutQuad);
    }

    /**
     * ��̫�������ʱ����Ӧ������
     * ̫�����ƶ���ָ�����յ�λ�ã����ڵ������������ͬʱ����Ϸ�����������Ӧ�ķ�����
     */
    void OnMouseDown() 
    { 
        // �ƶ���SunManagerָ�����ռ���λ�ã�Ȼ�������������ӷ���
        transform.DOMove(SunManager.Instance.GetSunPointTextPosition(), moveDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(
            () =>
            {
                Destroy(this.gameObject); // ����̫������
                SunManager.Instance.AddSun(point); // ����Ϸ��������ӷ���
            }
            );
    }

}

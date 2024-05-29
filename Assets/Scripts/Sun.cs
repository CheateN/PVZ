using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/**
 * 表示太阳的类，负责太阳的移动动画和交互逻辑。
 */
public class Sun : MonoBehaviour
{
    // 太阳移动到目标位置所需的时间
    public float moveDuration = 1;
    // 太阳被收集时增加的分数
    public int point = 50;

    /**
     * 使用线性运动的方式，将太阳移动到指定的位置。
     * @param targetPos 目标位置的向量3D坐标。
     */
    public void LinearTo(Vector3 targetPos)
    {
        transform.DOMove(targetPos, moveDuration); // 直线移动到目标位置
    }
    
    /**
     * 使用曲线运动的方式，将太阳“跳转”到指定的位置。
     * @param targetPos 目标位置的向量3D坐标。
     */
    public void JumpTo(Vector3 targetPos)
    {
        targetPos.z = -1; // 设置目标位置的z轴坐标为-1
        Vector3 centerPos = (transform.position + targetPos) / 2; // 计算中间位置
        float distance = Vector3.Distance(transform.position, targetPos); // 计算当前位置到目标位置的距离

        centerPos.y += (distance/2); // 根据距离调整中间位置的y轴坐标

        // 使用Catmull-Rom曲线从当前位置经由中间位置移动到目标位置
        transform.DOPath(new Vector3[] { transform.position, centerPos, targetPos },
            moveDuration, PathType.CatmullRom).SetEase(Ease.OutQuad);
    }

    /**
     * 当太阳被点击时的响应函数。
     * 太阳将移动到指定的终点位置，并在到达后销毁自身，同时向游戏管理器添加相应的分数。
     */
    void OnMouseDown() 
    { 
        // 移动到SunManager指定的收集点位置，然后销毁自身并增加分数
        transform.DOMove(SunManager.Instance.GetSunPointTextPosition(), moveDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(
            () =>
            {
                Destroy(this.gameObject); // 销毁太阳对象
                SunManager.Instance.AddSun(point); // 向游戏管理器添加分数
            }
            );
    }

}

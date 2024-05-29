using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 表示一个单元格的类，用于种植植物。
 */
public class Cell : MonoBehaviour
{
    // 当前单元格中种植的植物
    public Plant currentPlant;

    /**
     * 当鼠标点击单元格时调用。
     * 将点击事件传递给手部管理器。
     */
    private void OnMouseDown()
    {
        HandManager.Instance.OnCellClick(this);
    }

    /**
     * 尝试在单元格中添加一株植物。
     * @param plant 要添加的植物对象。
     * @return 如果成功添加植物返回true，如果单元格中已有植物则返回false。
     */
    public bool AddPlant(Plant plant)
    {
        // 如果当前单元格已有植物，则不添加
        if (currentPlant != null) return false;

        currentPlant = plant;
        currentPlant.transform.position = transform.position; // 设置植物位置与单元格相同
        plant.TransitionToEnable(); // 激活植物
        return true;
    }

}

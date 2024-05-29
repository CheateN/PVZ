using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 手部管理器，负责控制和管理游戏中的植物放置。
 */
public class HandManager : MonoBehaviour
{
    // 单例模式，用于确保游戏场景中只有一个HandManager实例
    public static HandManager Instance { get; private set; }

    // 存储所有植物预制体的列表
    public List<Plant> plantPrefabList;

    // 当前被选中的植物预制体实例
    private Plant currentPlant;

    // 在对象唤醒时初始化单例
    private void Awake()
    {
        Instance = this;
    }

    // 每帧更新，使当前植物跟随鼠标指针
    private void Update()
    {
        FollowCursor();
    }

    /**
     * 尝试添加一个植物到当前场景中。
     * @param plantType 植物类型。
     * @return 如果成功添加植物返回true，否则返回false。
     */
    public bool AddPlant(PlantType plantType)
    {
        // 如果已经有植物存在，则不能添加新植物
        if (currentPlant != null) return false;

        // 尝试获取对应植物类型的植物预制体
        Plant plantPrefab = GetPlantPrefab(plantType);
        if (plantPrefab == null)
        {
            // 如果无法获取植物预制体，则打印错误信息并返回false
            print("需要的植物预制体不存在"); return false;
        }
        // 实例化植物并设置为当前植物
        currentPlant = GameObject.Instantiate(plantPrefab);
        return true;
    }

    /**
     * 根据植物类型从植物预制体列表中获取对应的植物预制体。
     * @param plantType 植物类型。
     * @return 找到的植物预制体，如果未找到则返回null。
     */
    private Plant GetPlantPrefab(PlantType plantType)
    {
        foreach(Plant plant in plantPrefabList)
        {
            if (plant.plantType == plantType)
            {
                return plant;
            }
        }
        return null;
    }

    // 使当前植物位置跟随鼠标指针
    void FollowCursor()
    {
        // 如果没有选中的植物，则不执行任何操作
        if (currentPlant == null) return;

        // 计算鼠标在世界坐标系中的位置，并设置当前植物的位置
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        currentPlant.transform.position = mouseWorldPosition;

    }

    /**
     * 当玩家点击单元格时调用，尝试在指定单元格添加当前植物。
     * @param cell 被点击的单元格。
     */
    public void OnCellClick(Cell cell)
    {
        // 如果没有选中的植物，则不执行任何操作
        if (currentPlant == null) return;

        // 尝试在单元格中添加植物
        bool isSuccess = cell.AddPlant(currentPlant);

        // 如果添加成功，则清除当前植物，并播放音效
        if (isSuccess)
        {
            currentPlant = null;
            AudioManager.Instance.PlayClip(Config.plant);
        }
    }

}

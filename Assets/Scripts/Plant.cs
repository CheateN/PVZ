using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 植物状态枚举，定义植物的两种状态：禁用和启用
enum PlantState
{
    Disable, // 禁用状态
    Enable  // 启用状态
}

// 植物类，负责管理植物的基本行为和状态
public class Plant : MonoBehaviour
{
    // 植物的当前状态，默认为禁用
    PlantState plantState = PlantState.Disable;
    // 植物的类型，默认为向日葵
    public PlantType plantType = PlantType.Sunflower;

    // 植物的健康值，默认为100
    public int HP = 100;

    // 在植物对象初始化时调用，将植物状态设置为禁用，并禁用动画和碰撞器
    private void Start()
    {
        TransitionToDisable();
    }

    // 每帧更新时根据植物当前状态执行不同的更新逻辑
    private void Update()
    {
        switch (plantState)
        {
            case PlantState.Disable:
                DisableUpdate(); // 处理禁用状态下的更新
                break;
            case PlantState.Enable:
                EnableUpdate(); // 处理启用状态下的更新
                break;
            default:
                break;
        }
    }

    // 禁用状态下的更新逻辑，当前为空实现，子类可以通过覆盖此方法来提供具体逻辑
    void DisableUpdate()
    {

    }
    // 启用状态下的更新逻辑，当前为空实现，子类可以通过覆盖此方法来提供具体逻辑
    protected virtual void EnableUpdate()
    {

    }

    // 将植物状态转移到禁用，并禁用动画组件和2D碰撞器
    void TransitionToDisable()
    {
        plantState = PlantState.Disable;
        GetComponent<Animator>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    // 将植物状态转移到启用，并启用动画组件和2D碰撞器
    public void TransitionToEnable()
    {
        plantState = PlantState.Enable;
        GetComponent<Animator>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    // 对植物造成伤害，若健康值降至0或以下，则调用Die方法销毁植物对象
    public void TakeDamage(int damage)
    {
        this.HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    // 销毁植物游戏对象，实现植物死亡逻辑
    private void Die()
    {
        Destroy(gameObject);
    }
}


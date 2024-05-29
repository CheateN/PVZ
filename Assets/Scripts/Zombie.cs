using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 僵尸状态枚举，定义了僵尸的不同行为状态
enum ZombieState
{
    Move, // 移动状态
    Eat,  // 吃植物状态
    Die,  // 死亡状态
    Pause // 暂停状态
}

/// <summary>
/// 僵尸类，负责管理僵尸的行为、状态和属性。
/// </summary>
public class Zombie : MonoBehaviour
{
    ZombieState zombieState = ZombieState.Move; // 初始状态为移动
    private Rigidbody2D rgd;                    // 僵尸的物理体
    public float moveSpeed = 10;                // 僵尸的移动速度
    private Animator anim;                      // 僵尸的动画控制器

    public int atkValue = 30;                   // 僵尸的攻击力
    public float atkDuration = 2;               // 僵尸攻击持续时间
    private float atkTimer = 0;                 // 僵尸攻击计时器

    private Plant currentEatPlant;              // 当前被攻击的植物

    public int HP = 200;                        // 僵尸的初始血量
    private int currentHP;                      // 僵尸的当前血量
    public GameObject zombieHeadPrefab;         // 僵尸头颅的预制体

    private bool haveHead = true;               // 僵尸是否还有头颅

    private float headlessMoveSpeedMultiplier = 2;   // 失去头部后移动速度的提升倍数
    private int headlessAtkValueMultiplier = 2;   // 失去头部后攻击力的提升倍数

    // 在游戏开始时调用，初始化僵尸的物理体和动画控制器，设置初始血量
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHP = HP;
    }

    // 每帧调用一次，根据僵尸当前的状态更新僵尸的行为
    void Update()
    {
        switch (zombieState)
        {
            case ZombieState.Move:
                MoveUpdate();   // 移动状态更新
                break;
            case ZombieState.Eat:
                EatUpdate();    // 吃植物状态更新
                break;
            case ZombieState.Die:
                // 死亡状态不进行更新
                break;
            default:
                break;
        }
    }

    // 移动状态下的更新函数，使僵尸向左移动
    void MoveUpdate()
    {
        rgd.MovePosition(rgd.position + Vector2.left * (moveSpeed * Time.deltaTime));
    }

    // 吃植物状态下的更新函数，对当前植物造成伤害
    void EatUpdate()
    {
        atkTimer += Time.deltaTime;
        if (atkTimer > atkDuration && currentEatPlant != null)
        {
            AudioManager.Instance.PlayClip(Config.eat);
            currentEatPlant.TakeDamage(atkValue);
            atkTimer = 0;
        }
    }

    // 当僵尸触碰到植物时的响应函数，开始吃植物
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {
            anim.SetBool("IsAttacking", true);
            TransitionToEat();
            currentEatPlant = collision.GetComponent<Plant>();
        }
        else if (collision.tag == "House")
        {
            GameManager.Instance.GameEndFail(); // 当僵尸碰到房屋时，游戏失败
        }
    }

    // 当僵尸离开植物时的响应函数，停止吃植物
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {
            anim.SetBool("IsAttacking", false);
            zombieState = ZombieState.Move;
            currentEatPlant = null;
        }
    }

    // 转换到吃植物状态
    void TransitionToEat()
    {
        zombieState = ZombieState.Eat;
        atkTimer = 0;
    }

    // 转换到暂停状态
    public void TransitionToPause()
    {
        zombieState = ZombieState.Pause;
        anim.enabled = false; // 禁用动画控制器
    }

    // 僵尸受到伤害的函数，根据伤害减少血量，更新动画和状态
    public void TakeDamage(int damage)
    {
        if (currentHP <= 0) return;

        this.currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = -1;
            Dead(); // 调用死亡函数
        }
        float hpPercent = currentHP * 1f / HP;
        anim.SetFloat("HPPercent", hpPercent);
        if (hpPercent < .5f && haveHead)
        {
            haveHead = false;
            GameObject go = GameObject.Instantiate(zombieHeadPrefab, transform.position, Quaternion.identity);
            Destroy(go, 2); // 实例化并销毁僵尸头颅对象

            // 更新僵尸的速度和攻击力
            moveSpeed *= headlessMoveSpeedMultiplier;
            atkValue *= headlessAtkValueMultiplier;
        }
    }

    // 僵尸死亡的处理函数，禁用碰撞器、从僵尸管理器中移除僵尸、销毁游戏对象
    private void Dead()
    {
        if (zombieState == ZombieState.Die) return;

        zombieState = ZombieState.Die;
        GetComponent<Collider2D>().enabled = false;
        ZombieManager.Instance.RemoveZombie(this);

        Destroy(this.gameObject, 2);
    }
}

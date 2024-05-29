using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 豌豆子弹类，用于表示游戏中的豌豆子弹对象，具有攻击力和速度属性。
 */
public class PeaBullet : MonoBehaviour
{
    // 子弹的速度
    private float speed = 3;
    // 子弹的攻击力
    private int atkValue = 30;
    // 子弹击中目标时的特效预制体
    public GameObject peaBulletHitPrefab;

    /**
     * 设置子弹的攻击力。
     * @param atkValue 新的攻击力值。
     */
    public void SetATKValue(int atkValue)
    {
        this.atkValue = atkValue;
    }

    /**
     * 设置子弹的速度。
     * @param speed 新的速度值。
     */
    public void SetSpeed (float speed)
    {
        this.speed = speed;
    }

    // 在物体开始时调用，设定子弹的生命周期
    private void Start()
    {
        Destroy(gameObject, 10); // 10秒后自动销毁子弹对象
    }

    // 每帧更新位置，实现子弹的移动
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    /**
     * 当子弹与任何物体发生碰撞时调用。
     * 如果碰撞的是僵尸，则对僵尸造成伤害，并播放击中特效。
     * @param collision 碰撞的物体
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie") // 如果碰撞的是僵尸
        {
            Destroy(this.gameObject); // 销毁子弹
            collision.GetComponent<Zombie>().TakeDamage(atkValue); // 对僵尸造成伤害
            GameObject go = GameObject.Instantiate(peaBulletHitPrefab, transform.position, Quaternion.identity); // 播放击中特效
            Destroy(go, 1); // 1秒后销毁特效对象
        }
    }
}

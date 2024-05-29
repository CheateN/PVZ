using NUnit.Framework.Constraints; // 引入NUnit框架的约束命名空间
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Peashooter类：继承自Plant类，代表豌豆射手这一植物角色。
 */
public class Peashooter : Plant
{
    public float shootDuration = 2; // 射击间隔时间
    private float shootTimer = 0; // 当前射击计时器
    public Transform shootPointTransform; // 射击点的Transform
    public PeaBullet peaBulletPrefab; // 子弹预制体

    public float bulletSpeed = 5; // 子弹速度
    public int atkValue = 20; // 攻击力值

    /**
     * 启用更新时，控制豌豆射手的射击频率。
     */
    protected override void EnableUpdate()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer > shootDuration)
        {
            Shoot();
            shootTimer = 0;
        }
    }

    /**
     * 执行射击动作。
     */
    void Shoot()
    {
        AudioManager.Instance.PlayClip(Config.shoot); // 播放射击音效
        PeaBullet pb= GameObject.Instantiate(peaBulletPrefab, shootPointTransform.position, Quaternion.identity); // 实例化子弹
        pb.SetSpeed(bulletSpeed); // 设置子弹速度
        pb.SetATKValue(atkValue); // 设置子弹攻击力
    }
}

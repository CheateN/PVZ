using NUnit.Framework.Constraints; // ����NUnit��ܵ�Լ�������ռ�
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Peashooter�ࣺ�̳���Plant�࣬�����㶹������һֲ���ɫ��
 */
public class Peashooter : Plant
{
    public float shootDuration = 2; // ������ʱ��
    private float shootTimer = 0; // ��ǰ�����ʱ��
    public Transform shootPointTransform; // ������Transform
    public PeaBullet peaBulletPrefab; // �ӵ�Ԥ����

    public float bulletSpeed = 5; // �ӵ��ٶ�
    public int atkValue = 20; // ������ֵ

    /**
     * ���ø���ʱ�������㶹���ֵ����Ƶ�ʡ�
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
     * ִ�����������
     */
    void Shoot()
    {
        AudioManager.Instance.PlayClip(Config.shoot); // ���������Ч
        PeaBullet pb= GameObject.Instantiate(peaBulletPrefab, shootPointTransform.position, Quaternion.identity); // ʵ�����ӵ�
        pb.SetSpeed(bulletSpeed); // �����ӵ��ٶ�
        pb.SetATKValue(atkValue); // �����ӵ�������
    }
}

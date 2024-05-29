using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * �㶹�ӵ��࣬���ڱ�ʾ��Ϸ�е��㶹�ӵ����󣬾��й��������ٶ����ԡ�
 */
public class PeaBullet : MonoBehaviour
{
    // �ӵ����ٶ�
    private float speed = 3;
    // �ӵ��Ĺ�����
    private int atkValue = 30;
    // �ӵ�����Ŀ��ʱ����ЧԤ����
    public GameObject peaBulletHitPrefab;

    /**
     * �����ӵ��Ĺ�������
     * @param atkValue �µĹ�����ֵ��
     */
    public void SetATKValue(int atkValue)
    {
        this.atkValue = atkValue;
    }

    /**
     * �����ӵ����ٶȡ�
     * @param speed �µ��ٶ�ֵ��
     */
    public void SetSpeed (float speed)
    {
        this.speed = speed;
    }

    // �����忪ʼʱ���ã��趨�ӵ�����������
    private void Start()
    {
        Destroy(gameObject, 10); // 10����Զ������ӵ�����
    }

    // ÿ֡����λ�ã�ʵ���ӵ����ƶ�
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    /**
     * ���ӵ����κ����巢����ײʱ���á�
     * �����ײ���ǽ�ʬ����Խ�ʬ����˺��������Ż�����Ч��
     * @param collision ��ײ������
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie") // �����ײ���ǽ�ʬ
        {
            Destroy(this.gameObject); // �����ӵ�
            collision.GetComponent<Zombie>().TakeDamage(atkValue); // �Խ�ʬ����˺�
            GameObject go = GameObject.Instantiate(peaBulletHitPrefab, transform.position, Quaternion.identity); // ���Ż�����Ч
            Destroy(go, 1); // 1���������Ч����
        }
    }
}

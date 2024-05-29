using UnityEngine;

/**
 * ��ʾ���տ��࣬�̳���Plant�ࡣ���տ��ܹ��������⡣
 */
public class Sunflower : Plant
{
    public float produceDuration = 5; // ���տ�������������ڣ���λΪ��
    private float produceTimer = 0; // ����׷�����տ���������ļ�ʱ��
    private Animator anim; // ���տ��Ķ��������������ڿ��ƶ���״̬

    public GameObject sunPrefab; // ��������ϵͳ��Ԥ����󣬼���������ʱ����Ч

    public float jumpMinDistance = 0.3f; // ������Ծ����С����
    public float jumpMaxDistance = 2; // ������Ծ��������

    // �ڶ����ʼ��ʱ���ã����ڻ�ȡ���տ��Ķ���������
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /**
     * �����տ����ڼ���״̬ʱ��ÿ֡���ô˷��������ڿ�������Ĳ�����
     */
    protected override void EnableUpdate()
    {
        produceTimer += Time.deltaTime;

        // ����������ļ�ʱ�������趨����ʱ��������������Ķ���
        if (produceTimer > produceDuration)
        {
            produceTimer = 0;
            anim.SetTrigger("IsGlowing");
        }
    }

    /**
     * ���ڴ������տ����������Ч����
     */
    public void ProduceSun()
    { 
        // �����տ�λ�ô�ʵ������������ϵͳ����
        GameObject go = GameObject.Instantiate(sunPrefab, transform.position, Quaternion.identity);

        // �������������Ծ�ľ���
        float distance = Random.Range(jumpMinDistance, jumpMaxDistance);
        // �������������Ծ�ķ���������棩
        distance = Random.Range(0, 2) < 1 ? -distance : distance;
        // ����������Ծ�������λ��
        Vector3 position = transform.position;
        position.x += distance;

        // ������������λ�ã�ʹ���������տ�����������Ծ����
        go.GetComponent<Sun>().JumpTo(position);
    }
}

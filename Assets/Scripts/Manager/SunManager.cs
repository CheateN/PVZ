using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/**
 * ̫���������࣬����̫�������ɺ�̫�������Ĺ���
 */
public class SunManager : MonoBehaviour
{
    // ����ģʽ������ȫ��Ψһ����̫��������
    public static SunManager Instance { get; private set; }
    [SerializeField]
    private int sunPoint; // ��ǰ̫������
    public int SunPoint
    {
        get { return sunPoint; }
    }
    public TextMeshProUGUI sunPointText; // ̫���������ı���ʾ
    private Vector3 sunPointTextPosition; // ̫�������ı���λ��
    public float produceTime; // ̫�����ɵ�ʱ����
    private float produceTimer; // ����̫���ļ�ʱ��
    public GameObject sunPrefab; // ̫�������Ԥ��

    private bool isStartProduce = false; // �Ƿ�ʼ����̫��

    /**
     * Awake �������ڶ�����ʱ���У���������̫���������ĵ�����
     */
    private void Awake()
    {
        Instance = this;
    }

    /**
     * Start �������ڳ�����ʼʱ���У����ڳ�ʼ��̫�������ı��ͼ�����λ�á�
     */
    private void Start()
    {
        UpdateSunPointText();
        CalcSunPointTextPosition();
        //StartProduce();
    }

    /**
     * Update ������ÿ֡���£����ڴ�������̫�����߼���
     */
    private void Update()
    {
        if (isStartProduce)
        {
            ProduceSun();
        }
    }

    /**
     * ��ʼ����̫����
     */
    public void StartProduce()
    {
        isStartProduce = true;
    }

    /**
     * ֹͣ����̫����
     */
    public void StopProduce()
    {
        isStartProduce = false;
    }

    /**
     * ����̫���������ı���ʾ��
     */
    private void UpdateSunPointText()
    {
        sunPointText.text = SunPoint.ToString();
    }

    /**
     * ����ָ��������̫��������
     * @param point ��Ҫ���ٵ�̫��������
     */
    public void SubSun(int point)
    {
        sunPoint -= point;
        UpdateSunPointText();
    }

    /**
     * ����ָ��������̫��������
     * @param point ��Ҫ���ӵ�̫��������
     */
    public void AddSun(int point)
    {
        sunPoint += point;
        UpdateSunPointText();
    }

    /**
     * ��ȡ̫�������ı���λ�á�
     * @return ����̫�������ı���Vector3λ�á�
     */
    public Vector3 GetSunPointTextPosition()
    {
        return sunPointTextPosition;
    }

    /**
     * ���㲢����̫�������ı���λ�á�
     */
    private void CalcSunPointTextPosition()
    {
        Vector3 position= Camera.main.ScreenToWorldPoint(sunPointText.transform.position);
        position.z = 0;
        sunPointTextPosition = position;
    }

    /**
     * �����趨��ʱ��������̫����
     */
    void ProduceSun()
    {
        produceTimer += Time.deltaTime;
        if (produceTimer > produceTime)
        {
            produceTimer = 0;
            Vector3 position = new Vector3(Random.Range(-5, 6.5f), 6.2f, -1);
            GameObject go = GameObject.Instantiate(sunPrefab,position,Quaternion.identity);

            position.y = Random.Range(-4, 3f);
            go.GetComponent<Sun>().LinearTo(position);
        }
    }
}

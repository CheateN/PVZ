using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/**
 * 太阳管理器类，负责太阳的生成和太阳点数的管理。
 */
public class SunManager : MonoBehaviour
{
    // 单例模式，用于全局唯一访问太阳管理器
    public static SunManager Instance { get; private set; }
    [SerializeField]
    private int sunPoint; // 当前太阳点数
    public int SunPoint
    {
        get { return sunPoint; }
    }
    public TextMeshProUGUI sunPointText; // 太阳点数的文本显示
    private Vector3 sunPointTextPosition; // 太阳点数文本的位置
    public float produceTime; // 太阳生成的时间间隔
    private float produceTimer; // 生成太阳的计时器
    public GameObject sunPrefab; // 太阳对象的预设

    private bool isStartProduce = false; // 是否开始生成太阳

    /**
     * Awake 函数，在对象唤醒时运行，用于设置太阳管理器的单例。
     */
    private void Awake()
    {
        Instance = this;
    }

    /**
     * Start 函数，在场景开始时运行，用于初始化太阳点数文本和计算其位置。
     */
    private void Start()
    {
        UpdateSunPointText();
        CalcSunPointTextPosition();
        //StartProduce();
    }

    /**
     * Update 函数，每帧更新，用于处理生成太阳的逻辑。
     */
    private void Update()
    {
        if (isStartProduce)
        {
            ProduceSun();
        }
    }

    /**
     * 开始生成太阳。
     */
    public void StartProduce()
    {
        isStartProduce = true;
    }

    /**
     * 停止生成太阳。
     */
    public void StopProduce()
    {
        isStartProduce = false;
    }

    /**
     * 更新太阳点数的文本显示。
     */
    private void UpdateSunPointText()
    {
        sunPointText.text = SunPoint.ToString();
    }

    /**
     * 减少指定数量的太阳点数。
     * @param point 需要减少的太阳点数。
     */
    public void SubSun(int point)
    {
        sunPoint -= point;
        UpdateSunPointText();
    }

    /**
     * 增加指定数量的太阳点数。
     * @param point 需要增加的太阳点数。
     */
    public void AddSun(int point)
    {
        sunPoint += point;
        UpdateSunPointText();
    }

    /**
     * 获取太阳点数文本的位置。
     * @return 返回太阳点数文本的Vector3位置。
     */
    public Vector3 GetSunPointTextPosition()
    {
        return sunPointTextPosition;
    }

    /**
     * 计算并设置太阳点数文本的位置。
     */
    private void CalcSunPointTextPosition()
    {
        Vector3 position= Camera.main.ScreenToWorldPoint(sunPointText.transform.position);
        position.z = 0;
        sunPointTextPosition = position;
    }

    /**
     * 根据设定的时间间隔生成太阳。
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

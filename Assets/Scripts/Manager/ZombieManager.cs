using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 定义丧尸生成的状态
enum SpawnState
{
    NotStart, // 未开始
    Spawning, // 正在生成
    End // 结束
}
/**
 * 丧尸管理器，负责控制丧尸的生成和管理。
 */
public class ZombieManager : MonoBehaviour
{
    // 丧尸管理器的单例实例
    public static ZombieManager Instance { get; private set; }

    // 当前丧尸生成的状态
    private SpawnState spawnState = SpawnState.NotStart;

    // 可用的生成点数组
    public Transform[] spawnPointList;
    // 丧尸的预置对象
    public GameObject zombiePrefab;

    // 存储所有已生成的丧尸
    private List<Zombie> zombieList = new List<Zombie>();

    // 在对象唤醒时初始化单例
    private void Awake()
    {
        Instance = this;
    }
    // 开始时准备生成丧尸
    private void Start()
    {
        //StartSpawn();
    }

    // 每帧更新，检查是否所有丧尸都被消灭，如果是则结束游戏
    private void Update()
    {
        if (spawnState==SpawnState.End && zombieList.Count==0)
        {
            GameManager.Instance.GameEndSuccess();
        }
    }

    /**
     * 开始生成丧尸。
     */
    public void StartSpawn()
    {
        spawnState = SpawnState.Spawning;
        StartCoroutine(SpawnZombie());
    }

    /**
     * 暂停丧尸生成。
     */
    public void Pause()
    {
        spawnState = SpawnState.End;
        foreach(Zombie zombie in zombieList)
        {
            zombie.TransitionToPause();
        }
    }

    /**
     * 生成丧尸的协程。
     * @yield WaitForSeconds 控制生成间隔
     */
    IEnumerator SpawnZombie()
    { 
        // 第一波丧尸，共5只
        for(int i = 0; i < 5; i++)
        {
            SpawnARandomZombie();
            yield return new WaitForSeconds(3);
        }

        yield return new WaitForSeconds(1);
        // 第二波丧尸，共10只
        for (int i = 0; i < 10; i++)
        {
            SpawnARandomZombie();
            yield return new WaitForSeconds(3);
        }

        yield return new WaitForSeconds(1);
        // 第三波丧尸，共10只，播放最后一波音效
        AudioManager.Instance.PlayClip(Config.lastwave);
        for (int i = 0; i < 10; i++)
        {
            SpawnARandomZombie();
            yield return new WaitForSeconds(3);
        }
        spawnState = SpawnState.End; // 设置生成状态为结束
    }

    /**
     * 随机生成一只丧尸。
     */
    private void SpawnARandomZombie()
    {
        if (spawnState == SpawnState.Spawning)
        {
            int index = Random.Range(0, spawnPointList.Length); // 随机选择一个生成点
            GameObject go = GameObject.Instantiate(zombiePrefab, spawnPointList[index].position, Quaternion.identity); // 在选择的生成点生成丧尸

            zombieList.Add(go.GetComponent<Zombie>()); // 将生成的丧尸添加到列表中
            
            go.GetComponent<SpriteRenderer>().sortingOrder = spawnPointList[index].GetComponent<SpriteRenderer>().sortingOrder; // 设置丧尸的绘制顺序与生成点相同
        }
    }

    /**
     * 从列表中移除指定的丧尸。
     * @param zombie 要移除的丧尸对象
     */
    public void RemoveZombie(Zombie zombie)
    {
        zombieList.Remove(zombie);
    }

}


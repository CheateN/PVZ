using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ɥʬ���ɵ�״̬
enum SpawnState
{
    NotStart, // δ��ʼ
    Spawning, // ��������
    End // ����
}
/**
 * ɥʬ���������������ɥʬ�����ɺ͹���
 */
public class ZombieManager : MonoBehaviour
{
    // ɥʬ�������ĵ���ʵ��
    public static ZombieManager Instance { get; private set; }

    // ��ǰɥʬ���ɵ�״̬
    private SpawnState spawnState = SpawnState.NotStart;

    // ���õ����ɵ�����
    public Transform[] spawnPointList;
    // ɥʬ��Ԥ�ö���
    public GameObject zombiePrefab;

    // �洢���������ɵ�ɥʬ
    private List<Zombie> zombieList = new List<Zombie>();

    // �ڶ�����ʱ��ʼ������
    private void Awake()
    {
        Instance = this;
    }
    // ��ʼʱ׼������ɥʬ
    private void Start()
    {
        //StartSpawn();
    }

    // ÿ֡���£�����Ƿ�����ɥʬ��������������������Ϸ
    private void Update()
    {
        if (spawnState==SpawnState.End && zombieList.Count==0)
        {
            GameManager.Instance.GameEndSuccess();
        }
    }

    /**
     * ��ʼ����ɥʬ��
     */
    public void StartSpawn()
    {
        spawnState = SpawnState.Spawning;
        StartCoroutine(SpawnZombie());
    }

    /**
     * ��ͣɥʬ���ɡ�
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
     * ����ɥʬ��Э�̡�
     * @yield WaitForSeconds �������ɼ��
     */
    IEnumerator SpawnZombie()
    { 
        // ��һ��ɥʬ����5ֻ
        for(int i = 0; i < 5; i++)
        {
            SpawnARandomZombie();
            yield return new WaitForSeconds(3);
        }

        yield return new WaitForSeconds(1);
        // �ڶ���ɥʬ����10ֻ
        for (int i = 0; i < 10; i++)
        {
            SpawnARandomZombie();
            yield return new WaitForSeconds(3);
        }

        yield return new WaitForSeconds(1);
        // ������ɥʬ����10ֻ���������һ����Ч
        AudioManager.Instance.PlayClip(Config.lastwave);
        for (int i = 0; i < 10; i++)
        {
            SpawnARandomZombie();
            yield return new WaitForSeconds(3);
        }
        spawnState = SpawnState.End; // ��������״̬Ϊ����
    }

    /**
     * �������һֻɥʬ��
     */
    private void SpawnARandomZombie()
    {
        if (spawnState == SpawnState.Spawning)
        {
            int index = Random.Range(0, spawnPointList.Length); // ���ѡ��һ�����ɵ�
            GameObject go = GameObject.Instantiate(zombiePrefab, spawnPointList[index].position, Quaternion.identity); // ��ѡ������ɵ�����ɥʬ

            zombieList.Add(go.GetComponent<Zombie>()); // �����ɵ�ɥʬ��ӵ��б���
            
            go.GetComponent<SpriteRenderer>().sortingOrder = spawnPointList[index].GetComponent<SpriteRenderer>().sortingOrder; // ����ɥʬ�Ļ���˳�������ɵ���ͬ
        }
    }

    /**
     * ���б����Ƴ�ָ����ɥʬ��
     * @param zombie Ҫ�Ƴ���ɥʬ����
     */
    public void RemoveZombie(Zombie zombie)
    {
        zombieList.Remove(zombie);
    }

}


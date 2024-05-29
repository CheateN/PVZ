using DG.Tweening;
using UnityEngine;

/**
 * 游戏管理器，负责游戏的初始化、开始、结束等流程控制。
 */
public class GameManager : MonoBehaviour
{
    // 单例模式，用于全局访问GameManager实例
    public static GameManager Instance { get; private set; }

    // UI管理器，包括准备UI、卡牌列表UI、失败UI和胜利UI
    public PrepareUI prepareUI;
    public CardListUI cardListUI;
    public FailUI failUI;
    public WinUI winUI;

    // 标记游戏是否已经结束
    private bool isGameEnd = false;


    /**
     * Awake函数，在对象唤醒时调用，用于设置GameManager的单例实例
     */
    private void Awake()
    {
        // 设置当前实例为单例
        Instance = this;
    }

    /**
     * Start函数，在场景加载后调用，用于初始化游戏流程
     */
    private void Start()
    {
        // 开始游戏
        GameStart();
    }

    /**
     * GameStart函数，初始化游戏开始时的动画和逻辑
     */
    void GameStart()
    {
        // 获取当前相机位置，然后设置相机路径动画，动画完成后执行OnCameraMoveComplete函数
        Vector3 currentPositon = Camera.main.transform.position;
        Camera.main.transform.DOPath(
            new Vector3[] { currentPositon, new Vector3(5, 0, -10), currentPositon },
            3, PathType.Linear).OnComplete(OnCameraMoveComplete);
    }

    /**
     * GameEndFail函数，处理游戏失败的逻辑
     */
    public void GameEndFail()
    {
        // 如果游戏已经结束，则不重复执行
        if (isGameEnd == true) return;
        isGameEnd = true;
        // 显示失败UI，暂停僵尸生成，禁用卡牌列表，停止阳光生产
        failUI.Show();
        ZombieManager.Instance.Pause();
        cardListUI.DisableCardList();
        SunManager.Instance.StopProduce();

        // 播放失败音乐
        AudioManager.Instance.PlayClip(Config.lose_music);
    }

    /**
     * GameEndSuccess函数，处理游戏胜利的逻辑
     */
    public void GameEndSuccess()
    {
        // 如果游戏已经结束，则不重复执行
        if (isGameEnd == true) return;
        isGameEnd = true;
        // 显示胜利UI，禁用卡牌列表，停止阳光生产
        winUI.Show();
        cardListUI.DisableCardList();
        SunManager.Instance.StopProduce();

        // 播放胜利音乐
        AudioManager.Instance.PlayClip(Config.win_music);
    }

    /**
     * OnCameraMoveComplete函数，相机移动动画完成后调用，用于显示准备UI
     */
    void OnCameraMoveComplete()
    {
        // 显示准备UI，并在完成后执行OnPrepreUIComplete函数
        prepareUI.Show(OnPrepreUIComplete);
    }

    /**
     * OnPrepreUIComplete函数，准备UI显示完成后调用，用于开始游戏生产逻辑
     */
    void OnPrepreUIComplete()
    {
        // 启动阳光生产，开始僵尸生成，显示卡牌列表，播放背景音乐
        SunManager.Instance.StartProduce();
        ZombieManager.Instance.StartSpawn();
        cardListUI.ShowCardList();
        AudioManager.Instance.PlayBgm(Config.bgm1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * 控制启动场景的脚本。
 * 主要功能是处理开始游戏按钮的点击事件，加载下一个场景。
 */
public class StartSceneController : MonoBehaviour
{
    
    /**
     * 当点击开始游戏按钮时调用此方法。
     * 无参数。
     * 无返回值。
     * 功能：加载当前场景索引加1的场景，实现场景切换。
     */
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}

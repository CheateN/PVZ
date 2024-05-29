using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * 菜单场景控制器类，负责管理菜单场景中的交互和逻辑。
 */
public class MenuSceneController : MonoBehaviour
{
    public GameObject inputPanelGo; // 输入面板游戏对象，用于玩家输入名字。

    public TMP_InputField nameInputField; // 名字输入字段，玩家在此输入自己的名字。

    public TextMeshProUGUI nameText; // 显示玩家名字的文本，进入游戏前可看到自己的名字。

    private void Start()
    {
        UpdateNameUI(); // 初始化时调用，用于更新玩家名字的显示。
    }

    /**
     * 点击“更改名字”按钮时触发的函数。
     * 从PlayerPrefs中读取玩家名字，显示在输入字段中，并激活输入面板。
     */
    public void OnChangeNameButtonClick()
    {
        string name = PlayerPrefs.GetString("Name", "");
        nameInputField.text = name;
        inputPanelGo.SetActive(true);
        AudioManager.Instance.PlayClip(Config.btn_click);
    }

    /**
     * 点击“提交”按钮时触发的函数。
     * 将输入字段中的玩家名字保存到PlayerPrefs，并隐藏输入面板，更新名字UI。
     */
    public void OnSubmitButtonClick()
    {
        PlayerPrefs.SetString("Name", nameInputField.text);
        inputPanelGo.SetActive(false);
        UpdateNameUI();
        AudioManager.Instance.PlayClip(Config.btn_click);
    }

    /**
     * 更新名字UI的函数。
     * 从PlayerPrefs中读取玩家名字，并显示在名字文本上。
     */
    void UpdateNameUI()
    {
        string name = PlayerPrefs.GetString("Name", "-");
        nameText.text = name;
    }

    /**
     * 点击“冒险开始”按钮时触发的函数。
     * 播放按钮点击音效，加载下一个场景。
     */
    public void OnAdventureButtonClick()
    {
        AudioManager.Instance.PlayClip(Config.btn_click);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * �˵������������࣬�������˵������еĽ������߼���
 */
public class MenuSceneController : MonoBehaviour
{
    public GameObject inputPanelGo; // ���������Ϸ������������������֡�

    public TMP_InputField nameInputField; // ���������ֶΣ�����ڴ������Լ������֡�

    public TextMeshProUGUI nameText; // ��ʾ������ֵ��ı���������Ϸǰ�ɿ����Լ������֡�

    private void Start()
    {
        UpdateNameUI(); // ��ʼ��ʱ���ã����ڸ���������ֵ���ʾ��
    }

    /**
     * ������������֡���ťʱ�����ĺ�����
     * ��PlayerPrefs�ж�ȡ������֣���ʾ�������ֶ��У�������������塣
     */
    public void OnChangeNameButtonClick()
    {
        string name = PlayerPrefs.GetString("Name", "");
        nameInputField.text = name;
        inputPanelGo.SetActive(true);
        AudioManager.Instance.PlayClip(Config.btn_click);
    }

    /**
     * ������ύ����ťʱ�����ĺ�����
     * �������ֶ��е�������ֱ��浽PlayerPrefs��������������壬��������UI��
     */
    public void OnSubmitButtonClick()
    {
        PlayerPrefs.SetString("Name", nameInputField.text);
        inputPanelGo.SetActive(false);
        UpdateNameUI();
        AudioManager.Instance.PlayClip(Config.btn_click);
    }

    /**
     * ��������UI�ĺ�����
     * ��PlayerPrefs�ж�ȡ������֣�����ʾ�������ı��ϡ�
     */
    void UpdateNameUI()
    {
        string name = PlayerPrefs.GetString("Name", "-");
        nameText.text = name;
    }

    /**
     * �����ð�տ�ʼ����ťʱ�����ĺ�����
     * ���Ű�ť�����Ч��������һ��������
     */
    public void OnAdventureButtonClick()
    {
        AudioManager.Instance.PlayClip(Config.btn_click);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * �������������Ľű���
 * ��Ҫ�����Ǵ���ʼ��Ϸ��ť�ĵ���¼���������һ��������
 */
public class StartSceneController : MonoBehaviour
{
    
    /**
     * �������ʼ��Ϸ��ťʱ���ô˷�����
     * �޲�����
     * �޷���ֵ��
     * ���ܣ����ص�ǰ����������1�ĳ�����ʵ�ֳ����л���
     */
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}

using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHoverEffect : MonoBehaviour
{
    private Vector3 originalScale;  // ��Ҵ�������ͧ����
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f);  // ��Ҵ����� hover

    void Start()
    {
        // �ѹ�֡��Ҵ����ͧ����
        originalScale = transform.localScale;
    }

    // ���¡����� hover ���
    public void OnHoverEnter()
    {
        transform.localScale = hoverScale;
    }

    // ���¡����� hover �͡
    public void OnHoverExit()
    {
        transform.localScale = originalScale;
    }

    public void OnclickStart()
    {
        SceneManager.LoadScene("LevelPage");
       
    }

    public void OnclickLevel1()
    {
        SceneManager.LoadScene("LevelOne");
        Time.timeScale = 1.0f;
    }

    public void OnclickLevel2()
    {
        SceneManager.LoadScene("LevelTwo");
        Time.timeScale = 1.0f;
    }

    public void OnclickLevel3()
    {
        SceneManager.LoadScene("LevelThree");
        Time.timeScale = 1.0f;
    }

    public void ExitGame()
    {
        // ��Ǩ�ͺ��ҡ��ѧ�ӧҹ� Unity Editor �������
    #if UNITY_EDITOR
        // � Unity Editor ��Ҩ�������ش������ (Play Mode)
        UnityEditor.EditorApplication.isPlaying = false;
    #else
    // ����� Build �ͧ�����͡�ҡ��
    Application.Quit();
    #endif
    }
}

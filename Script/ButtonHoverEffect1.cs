using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHoverEffect : MonoBehaviour
{
    private Vector3 originalScale;  // ขนาดดั้งเดิมของปุ่ม
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f);  // ขนาดเมื่อ hover

    void Start()
    {
        // บันทึกขนาดเดิมของปุ่ม
        originalScale = transform.localScale;
    }

    // เรียกเมื่อ hover เข้า
    public void OnHoverEnter()
    {
        transform.localScale = hoverScale;
    }

    // เรียกเมื่อ hover ออก
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
        // ตรวจสอบว่ากำลังทำงานใน Unity Editor หรือไม่
    #if UNITY_EDITOR
        // ใน Unity Editor เราจะใช้การหยุดการเล่น (Play Mode)
        UnityEditor.EditorApplication.isPlaying = false;
    #else
    // ถ้าเป็น Build ของเกมจะออกจากเกม
    Application.Quit();
    #endif
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    public Slider Slider;  // แถบพลังชีวิตที่เป็น UI Slider
    public Color Low;  // สีของแถบพลังชีวิตเมื่อพลังชีวิตต่ำ
    public Color High;  // สีของแถบพลังชีวิตเมื่อพลังชีวิตสูง
    public Vector3 Offset;  // ระยะเลื่อนของแถบพลังชีวิตจากตำแหน่งศัตรู

    // ฟังก์ชันสำหรับตั้งค่าพลังชีวิตในแถบพลังชีวิต

    void Start()
    {
        float initialHealth = 100f;  // กำหนดพลังชีวิตเริ่มต้น
        float maxHealth = 100f;  // กำหนดพลังชีวิตสูงสุด
        SetHealth(initialHealth, maxHealth);  // เรียกฟังก์ชันเพื่อกำหนดค่าพลังชีวิต
    }
    public void SetHealth(float health, float maxHealth)
    {
        Slider.maxValue = maxHealth;  // ตั้งค่า maxValue ก่อน
        Slider.value = health;  // จากนั้นค่อยตั้งค่า value

        // อ้างอิงไปยัง Image ของ fillRect
        Image fillImage = Slider.fillRect.GetComponent<Image>();

        if (fillImage != null)
        {
            // เปลี่ยนสีตามค่าพลังชีวิต
            fillImage.color = Color.Lerp(Low, High, Slider.normalizedValue);
        }

        Color newColor = Color.Lerp(Low, High, Slider.normalizedValue);
        newColor.a = 1.0f;  // บังคับให้สีไม่โปร่งใส
        fillImage.color = newColor;
    }


    // ฟังก์ชันสำหรับตั้งค่าตำแหน่งของแถบพลังชีวิต
    public void SetPosition(Vector3 newPosition)
    {
        Slider.transform.position = newPosition;
    }

    void Update()
    {
        // เรียกใช้ฟังก์ชัน SetPosition เพื่อกำหนดตำแหน่งแถบพลังชีวิตในรูปแบบ screen space
        SetPosition(Camera.main.WorldToScreenPoint(transform.parent.position + Offset));
    }
}

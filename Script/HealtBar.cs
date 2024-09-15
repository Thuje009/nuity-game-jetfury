using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // นำเข้า namespace ของ TextMesh Pro สำหรับการใช้งาน TMP_Text

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarFill;  // อ้างอิงถึง UI Image ที่จะใช้แสดงแถบเลือด
    [SerializeField] private PlayerScript playerScript; // อ้างอิงถึง PlayerScript เพื่อดึงข้อมูลเกี่ยวกับพลังชีวิตของผู้เล่น
    [SerializeField] private RectMask2D _mask; // ตัวแปรสำหรับการจัดการการครอบคลุมของ RectMask2D (ถ้ามีการใช้งาน)
    [SerializeField] private TMP_Text _hpIndicator; // ตัวแปรสำหรับแสดงค่า HP ของผู้เล่นแบบตัวอักษรโดยใช้ TextMesh Pro (TMP_Text)

    private float _maxRightMask; // เก็บค่าสูงสุดสำหรับการตั้งค่า RectMask2D (ใช้ได้ถ้าต้องการ)
    private float _initialRightMask; // เก็บค่าเริ่มต้นของ RectMask2D (ใช้ได้ถ้าต้องการ)

    private void Start()
    {
        // ตรวจสอบว่ามีการตั้งค่า PlayerScript และ _hpIndicator หรือไม่ ถ้าไม่มีให้แสดง Error ใน Console
        if (playerScript == null || _hpIndicator == null)
        {
            Debug.LogError("PlayerScript หรือ HP Indicator ไม่ได้ถูกอ้างอิง!");
            return;
        }

        // ตั้งค่าข้อความของ _hpIndicator เพื่อแสดงค่า HP เริ่มต้นของผู้เล่น
        _hpIndicator.text = $"{playerScript.CurrentHealth}/{playerScript.maxHealth}";
    }

    void Update()
    {
        // ตรวจสอบว่ามีการตั้งค่า PlayerScript และ healthBarFill หรือไม่ ถ้าไม่มีให้หยุดการทำงานของฟังก์ชัน
        if (playerScript == null || healthBarFill == null)
        {
            return;
        }

        // คำนวณค่า fillValue โดยเอาพลังชีวิตปัจจุบันหารด้วยพลังชีวิตสูงสุด แล้วทำให้ค่าถูกจำกัดให้อยู่ระหว่าง 0 ถึง 1
        float fillValue = Mathf.Clamp01((float)playerScript.CurrentHealth / playerScript.maxHealth);

        // ตั้งค่า fillAmount ของ healthBarFill ซึ่งจะกำหนดขนาดการแสดงของแถบเลือด
        healthBarFill.fillAmount = fillValue;

        // อัพเดตข้อความใน _hpIndicator เพื่อแสดงค่า HP ปัจจุบันของผู้เล่น
        _hpIndicator.text = $"{playerScript.CurrentHealth}/{playerScript.maxHealth}";
    }
}

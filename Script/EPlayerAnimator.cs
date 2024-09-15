using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public float shootingInterval = 1f; // ระยะเวลาระหว่างการยิงแต่ละครั้ง

    void Start()
    {
        // เรียกใช้ฟังก์ชัน Shoot ทุกๆ shootingInterval วินาทีทันทีเมื่อเริ่มเกม
        InvokeRepeating("Shoot", 0f, shootingInterval);
    }

    void Shoot()
    {
        // เรียกใช้การยิง
        animator.SetTrigger("Eshoot");
    }

    void Update()
    {

    }
}

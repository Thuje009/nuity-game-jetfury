using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerScript : MonoBehaviour
{
    public float speed = 2f;
    public float maxHeight = 2f;
    public float minHeight = 1f;
    private Rigidbody2D rb;
    private bool movingUp = true;
    private bool isDead = false;
    private Animator animator;

    public HealthBarEnemy healthBar;  // อ้างอิงถึงแถบพลังชีวิต
    public float MaxHitpoints = 100f;  // พลังชีวิตสูงสุด
    private float Hitpoints;  // พลังชีวิตปัจจุบัน

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Hitpoints = MaxHitpoints;  // กำหนดพลังชีวิตเริ่มต้น

        // อัพเดตแถบพลังชีวิตเมื่อเริ่มต้น
        if (healthBar != null)
        {
            healthBar.SetHealth(Hitpoints, MaxHitpoints);
        }

        int enemyLayer = LayerMask.NameToLayer("Enemy");

        // ตรวจสอบว่ามีการตั้งค่า Layer ถูกต้อง
        if (enemyLayer == -1)
        {
            return;
        }

        // ไม่ให้ศัตรูใน Layer 'Enemy' ชนกันเอง
        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer);

    }

    void Update()
    {
        if (!isDead)
        {
            // ตรวจสอบทิศทางการเคลื่อนที่
            if (transform.position.y >= maxHeight)
            {
                movingUp = false;
            }
            else if (transform.position.y <= minHeight)
            {
                movingUp = true;
            }

            // เคลื่อนที่ขึ้นหรือลงตามทิศทางที่กำหนด
            if (movingUp)
            {
                rb.velocity = new Vector2(0, speed);
            }
            else
            {
                rb.velocity = new Vector2(0, -speed);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("bullet"))
        {
            // ทำลายกระสุนถ้าชน
            if (collision.gameObject.CompareTag("bullet"))
            {
                Destroy(collision.gameObject);
                TakeDamage(35); 
            }

            // ตรวจสอบการตาย
            if (!isDead && Hitpoints <= 0)
            {
                Die();
            }
        }
    }

    // ฟังก์ชันสำหรับลดพลังชีวิต
    private void TakeDamage(float damage)
    {
        Hitpoints -= damage;

        // อัพเดตแถบพลังชีวิต
        healthBar.SetHealth(Hitpoints, MaxHitpoints);


        if (Hitpoints <= 0 && !isDead)
        {
            Die();
        }
    }

    // ฟังก์ชันสำหรับจัดการเมื่อศัตรูตาย
    private void Die()
    {
        if (isDead) return;  // ตรวจสอบว่าศัตรูตายแล้วหรือไม่

        isDead = true;
        animator.SetTrigger("Edead");  // เรียกใช้แอนิเมชันการตาย
        rb.velocity = Vector2.zero;  // หยุดการเคลื่อนที่
        rb.isKinematic = false;
        rb.gravityScale = 2;  // เพิ่มแรงโน้มถ่วง

        // ทำลายศัตรูหลังจากแอนิเมชันการตายจบลง (สามารถตั้งเวลา delay ได้)
        Destroy(gameObject, 2f);  // ลบศัตรูออกจากเกมหลังจาก 2 วินาที

        // เรียกฟังก์ชันจาก PlayerScript
        PlayerScript playerScript = FindObjectOfType<PlayerScript>();
        if (playerScript != null)
        {
            playerScript.IncreaseEnemyKillCount();
        }
    }
}

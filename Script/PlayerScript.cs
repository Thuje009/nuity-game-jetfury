using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public float moveSpeed;
    public float maxHeight;
    public float fallSpeed;
    public float deathAnimationDuration = 1f;
    public bool isDead = false;

    public int requiredEnemyKills = 8; // Number of enemy kills required to win
    private int enemyKillCount = 0; // Current number of enemy kills

    public bool hasStartedMoving = false; // สถานะสำหรับตรวจสอบว่าผู้เล่นเริ่มขยับหรือยัง

    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject gameWin;




    // Health-related fields
    public int maxHealth = 100;
    public int CurrentHealth { get; private set; } // ใช้ Property สำหรับเข้าถึง CurrentHealth ได้ง่ายขึ้น

    public void IncreaseEnemyKillCount()
    {
        enemyKillCount++; // Increase the number of kills
    }




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        CurrentHealth = maxHealth;

        // ซ่อน Game Over UI ตอนเริ่มเกม
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        if (gameWin != null) 
        {
            gameWin.SetActive(false);
        }

        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 0;  //

    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (isDead)
        {
            return; // ถ้า Player ตายแล้ว จะไม่ทำการอัปเดตการเคลื่อนไหว
        }

        // ตรวจสอบว่าผู้เล่นได้เริ่มขยับหรือยัง
        if (!hasStartedMoving && (moveHorizontal != 0 || moveVertical != 0))
        {
            hasStartedMoving = true; // ตั้งค่าสถานะเมื่อผู้เล่นเริ่มขยับ
            rb.gravityScale = 1;     // เปิดการทำงานของแรงโน้มถ่วง
        }

        if (!hasStartedMoving)
        {
            // ถ้าผู้เล่นยังไม่ได้เริ่มขยับ จะไม่ทำการเคลื่อนที่
            return;
        }

        //Vector2 movement = new Vector2(moveHorizontal, moveVertical);


        if (moveHorizontal >= 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

            // ตรวจสอบว่าการเคลื่อนที่ในแกน Y ไม่เกิน maxHeight
            if (rb.position.y + movement.y * moveSpeed * Time.deltaTime > maxHeight)
            {
                movement.y = 0; // ป้องกันไม่ให้เคลื่อนที่ไปสูงเกินไป
            }

            rb.velocity = movement * moveSpeed;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }


        // ตรวจสอบถ้าไม่มีการกดขึ้นให้ตกอัตโนมัติ
        if (Input.GetAxis("Vertical") <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - fallSpeed * Time.deltaTime);
        }
    }


    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("ebullet"))
        {
            TakeDamage(20);
        }
        else if (target.gameObject.CompareTag("FinnishLine"))
        {
            // Check if the number of destroyed enemies is sufficient
            if (enemyKillCount >= requiredEnemyKills)
            {
                StartCoroutine(ShowGameWinAfterDelay(1f));
            }
            else
            {
                // Player has not destroyed enough enemies
                ShowGameOverUI(); // Show the game over UI
            }
        }
        else if (target.gameObject.CompareTag("background"))
        {
            ShowGameOverUI(); // Show the game over UI
        }
    }

    IEnumerator ShowGameWinAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // รอ 2 วินาที
        gameWin.SetActive(true);                 // แสดง UI ของชัยชนะ
        Time.timeScale = 0;                      // หยุดเกม
    }


    void TakeDamage(int damage)
    {
        CurrentHealth -= damage;  // ลดเลือดตามค่า damage ที่กำหนด

        if (CurrentHealth <= 0)
        {
            Die();  // เรียกฟังก์ชันการตายเมื่อเลือดหมด
        }
    }





    void ShowGameOverUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0f; // หยุดเกม
        }
    }

    void Die()
    {
        if (!isDead)
        {
            isDead = true; // ผู้เล่นตายแล้ว
            animator.SetTrigger("dead"); // แสดงแอนิเมชันตาย
            rb.velocity = Vector2.zero; // หยุดการเคลื่อนที่

            StartCoroutine(StopGameAfterDelay(1f)); // เรียก Coroutine เพื่อหยุดเกมหลังจาก 1 วินาที
        }
    }

    IEnumerator StopGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // รอ 1.5 วินาที
        Time.timeScale = 0f; // หยุดเกมโดยการตั้งค่า Time.timeScale เป็น 0
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);  // เปิด UI ของ Game Over
        }
    }
}

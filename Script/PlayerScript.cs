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

    public bool hasStartedMoving = false; // ʶҹ�����Ѻ��Ǩ�ͺ��Ҽ������������Ѻ�����ѧ

    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject gameWin;




    // Health-related fields
    public int maxHealth = 100;
    public int CurrentHealth { get; private set; } // �� Property ����Ѻ��Ҷ֧ CurrentHealth ����¢��

    public void IncreaseEnemyKillCount()
    {
        enemyKillCount++; // Increase the number of kills
    }




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        CurrentHealth = maxHealth;

        // ��͹ Game Over UI �͹�������
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
            return; // ��� Player ������� �����ӡ���ѻവ�������͹���
        }

        // ��Ǩ�ͺ��Ҽ��������������Ѻ�����ѧ
        if (!hasStartedMoving && (moveHorizontal != 0 || moveVertical != 0))
        {
            hasStartedMoving = true; // ��駤��ʶҹ�����ͼ������������Ѻ
            rb.gravityScale = 1;     // �Դ��÷ӧҹ�ͧ�ç�����ǧ
        }

        if (!hasStartedMoving)
        {
            // ��Ҽ������ѧ������������Ѻ �����ӡ������͹���
            return;
        }

        //Vector2 movement = new Vector2(moveHorizontal, moveVertical);


        if (moveHorizontal >= 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

            // ��Ǩ�ͺ��ҡ������͹����᡹ Y ����Թ maxHeight
            if (rb.position.y + movement.y * moveSpeed * Time.deltaTime > maxHeight)
            {
                movement.y = 0; // ��ͧ�ѹ����������͹�����٧�Թ�
            }

            rb.velocity = movement * moveSpeed;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }


        // ��Ǩ�ͺ�������ա�á������鵡�ѵ��ѵ�
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
        yield return new WaitForSeconds(delay);  // �� 2 �Թҷ�
        gameWin.SetActive(true);                 // �ʴ� UI �ͧ��ª��
        Time.timeScale = 0;                      // ��ش��
    }


    void TakeDamage(int damage)
    {
        CurrentHealth -= damage;  // Ŵ���ʹ������ damage ����˹�

        if (CurrentHealth <= 0)
        {
            Die();  // ���¡�ѧ��ѹ��õ����������ʹ���
        }
    }





    void ShowGameOverUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0f; // ��ش��
        }
    }

    void Die()
    {
        if (!isDead)
        {
            isDead = true; // �����蹵������
            animator.SetTrigger("dead"); // �ʴ��͹����ѹ���
            rb.velocity = Vector2.zero; // ��ش�������͹���

            StartCoroutine(StopGameAfterDelay(1f)); // ���¡ Coroutine ������ش����ѧ�ҡ 1 �Թҷ�
        }
    }

    IEnumerator StopGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // �� 1.5 �Թҷ�
        Time.timeScale = 0f; // ��ش���¡�õ�駤�� Time.timeScale �� 0
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);  // �Դ UI �ͧ Game Over
        }
    }
}

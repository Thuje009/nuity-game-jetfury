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

    public HealthBarEnemy healthBar;  // ��ҧ�ԧ�֧ᶺ��ѧ���Ե
    public float MaxHitpoints = 100f;  // ��ѧ���Ե�٧�ش
    private float Hitpoints;  // ��ѧ���Ե�Ѩ�غѹ

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Hitpoints = MaxHitpoints;  // ��˹���ѧ���Ե�������

        // �Ѿവᶺ��ѧ���Ե������������
        if (healthBar != null)
        {
            healthBar.SetHealth(Hitpoints, MaxHitpoints);
        }

        int enemyLayer = LayerMask.NameToLayer("Enemy");

        // ��Ǩ�ͺ����ա�õ�駤�� Layer �١��ͧ
        if (enemyLayer == -1)
        {
            return;
        }

        // �������ѵ��� Layer 'Enemy' ���ѹ�ͧ
        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer);

    }

    void Update()
    {
        if (!isDead)
        {
            // ��Ǩ�ͺ��ȷҧ�������͹���
            if (transform.position.y >= maxHeight)
            {
                movingUp = false;
            }
            else if (transform.position.y <= minHeight)
            {
                movingUp = true;
            }

            // ����͹���������ŧ�����ȷҧ����˹�
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
            // ����¡���ع��Ҫ�
            if (collision.gameObject.CompareTag("bullet"))
            {
                Destroy(collision.gameObject);
                TakeDamage(35); 
            }

            // ��Ǩ�ͺ��õ��
            if (!isDead && Hitpoints <= 0)
            {
                Die();
            }
        }
    }

    // �ѧ��ѹ����ѺŴ��ѧ���Ե
    private void TakeDamage(float damage)
    {
        Hitpoints -= damage;

        // �Ѿവᶺ��ѧ���Ե
        healthBar.SetHealth(Hitpoints, MaxHitpoints);


        if (Hitpoints <= 0 && !isDead)
        {
            Die();
        }
    }

    // �ѧ��ѹ����Ѻ�Ѵ���������ѵ�ٵ��
    private void Die()
    {
        if (isDead) return;  // ��Ǩ�ͺ����ѵ�ٵ�������������

        isDead = true;
        animator.SetTrigger("Edead");  // ���¡���͹����ѹ��õ��
        rb.velocity = Vector2.zero;  // ��ش�������͹���
        rb.isKinematic = false;
        rb.gravityScale = 2;  // �����ç�����ǧ

        // ������ѵ����ѧ�ҡ�͹����ѹ��õ�¨�ŧ (����ö������� delay ��)
        Destroy(gameObject, 2f);  // ź�ѵ���͡�ҡ����ѧ�ҡ 2 �Թҷ�

        // ���¡�ѧ��ѹ�ҡ PlayerScript
        PlayerScript playerScript = FindObjectOfType<PlayerScript>();
        if (playerScript != null)
        {
            playerScript.IncreaseEnemyKillCount();
        }
    }
}

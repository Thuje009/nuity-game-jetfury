using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint; // ��С�ȵ���� firePoint
    public GameObject bulletPrefab; // ��С�� prefab �ͧ����ع
    public float bulletForce = 30f; // �������Ǣͧ����ع
    public float maxDistance = 50f; // �����٧�ش������ع������͹���

    public AudioClip bulletSound;
    private AudioSource audioSource;

    private void Start()
    {
   
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // ��Ǩ�ͺ��Ҽ����蹡������ԧ
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            Vector2 direction = GetMouseDirection();
            rb.velocity = direction * bulletForce;
            StartCoroutine(DestroyBulletAfterDistance(bullet));

            // ��Ǩ�ͺ��ҷ�� audioSource ��� bulletSound �١��˹��������
            if (audioSource != null && bulletSound != null)
            {
                audioSource.PlayOneShot(bulletSound);
            }
            else
            {
                // �ʴ� Debug �ҡ audioSource ���� bulletSound �� null
                if (audioSource == null)
                {
                    Debug.LogWarning("AudioSource is missing!");
                }
                if (bulletSound == null)
                {
                    Debug.LogWarning("Bullet sound is missing!");
                }
            }
        }
    }

    Vector2 GetMouseDirection()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0; // ��Ѻ��� z ����� 0 ������ҡ��ѧ�ӧҹ� 2D
        Vector2 direction = (mouseWorldPosition - firePoint.position).normalized;
        return direction;
    }

    IEnumerator DestroyBulletAfterDistance(GameObject bullet)
    {
        while (bullet != null && Vector2.Distance(firePoint.position, bullet.transform.position) < maxDistance)
        {
            // ��Ǩ�ͺ��ҡ���ع�ѧ����������ѧ���١�����
            if (bullet == null)
            {
                yield break; // �͡�ҡ Coroutine ��ҡ���ع�١�����
            }
            yield return null;
        }

        if (bullet != null)
        {
            Destroy(bullet);
        }
    }
}

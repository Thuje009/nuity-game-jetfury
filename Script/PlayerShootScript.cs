using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint; // ประกาศตัวแปร firePoint
    public GameObject bulletPrefab; // ประกาศ prefab ของกระสุน
    public float bulletForce = 30f; // ความเร็วของกระสุน
    public float maxDistance = 50f; // ระยะสูงสุดที่กระสุนจะเคลื่อนที่

    public AudioClip bulletSound;
    private AudioSource audioSource;

    private void Start()
    {
   
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // ตรวจสอบว่าผู้เล่นกดปุ่มยิง
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

            // ตรวจสอบว่าทั้ง audioSource และ bulletSound ถูกกำหนดหรือไม่
            if (audioSource != null && bulletSound != null)
            {
                audioSource.PlayOneShot(bulletSound);
            }
            else
            {
                // แสดง Debug หาก audioSource หรือ bulletSound เป็น null
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
        mouseWorldPosition.z = 0; // ปรับค่า z ให้เป็น 0 เพราะเรากำลังทำงานใน 2D
        Vector2 direction = (mouseWorldPosition - firePoint.position).normalized;
        return direction;
    }

    IEnumerator DestroyBulletAfterDistance(GameObject bullet)
    {
        while (bullet != null && Vector2.Distance(firePoint.position, bullet.transform.position) < maxDistance)
        {
            // ตรวจสอบว่ากระสุนยังมีอยู่และยังไม่ถูกทำลาย
            if (bullet == null)
            {
                yield break; // ออกจาก Coroutine ถ้ากระสุนถูกทำลาย
            }
            yield return null;
        }

        if (bullet != null)
        {
            Destroy(bullet);
        }
    }
}

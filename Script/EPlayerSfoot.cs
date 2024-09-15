using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPlayerShoot : MonoBehaviour
{
    public Transform firePoint; // ตำแหน่งที่ยิงกระสุนออกมา
    public GameObject bulletPrefab; // Prefab ของกระสุน
    public float bulletForce = 30f; // ความเร็วของกระสุน
    public float maxDistance = 50f; // ระยะสูงสุดที่กระสุนจะเคลื่อน    ที่
    public float shootInterval ; // ระยะเวลาห่างกันระหว่างการยิงแต่ละครั้ง
    public float shootingRange = 5f; // ระยะทางที่ศัตรูสามารถยิงผู้เล่นได้
    public LayerMask playerLayer; // เลเยอร์ของผู้เล่น (ใช้สำหรับการตรวจสอบการชน)


    void Start()
    {
        // ตรวจสอบผู้เล่นและยิงทุกๆ shootInterval วินาที
        InvokeRepeating("CheckPlayerInRangeAndShoot", 0f, shootInterval); 
    }

    void CheckPlayerInRangeAndShoot()
    {
        // วาดเส้นใน Scene view (Debugging code only visible in Scene view)
        Debug.DrawRay(firePoint.position, firePoint.right * shootingRange, Color.red);

        Collider2D playerCollider = Physics2D.OverlapCircle(firePoint.position, shootingRange, LayerMask.GetMask("Player"));

        if (playerCollider != null)
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

            // ให้กระสุนพุ่งไปข้างหน้าตามทิศทางของ firePoint
            rb.AddForce(-firePoint.right * bulletForce, ForceMode2D.Impulse);

            // เริ่ม Coroutine เพื่อตรวจสอบและทำลายกระสุนเมื่อถึงระยะสูงสุด
            StartCoroutine(DestroyBulletAfterDistance(bullet));
        }
    }

    IEnumerator DestroyBulletAfterDistance(GameObject bullet)
    {
        // รอจนกว่ากระสุนจะเคลื่อนที่ถึงระยะสูงสุด
        while (bullet != null && Vector2.Distance(firePoint.position, bullet.transform.position) < maxDistance)
        {
            yield return null;
        }

        // ทำลายกระสุนเมื่อเกินระยะที่กำหนด
        if (bullet != null)
        {
            Destroy(bullet);
        }
    }

    // แสดงระยะยิงใน Unity Editor เพื่อให้ง่ายต่อการปรับแต่ง
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePoint.position, shootingRange);
    }

}

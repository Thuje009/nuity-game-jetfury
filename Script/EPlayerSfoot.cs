using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPlayerShoot : MonoBehaviour
{
    public Transform firePoint; // ���˹觷���ԧ����ع�͡��
    public GameObject bulletPrefab; // Prefab �ͧ����ع
    public float bulletForce = 30f; // �������Ǣͧ����ع
    public float maxDistance = 50f; // �����٧�ش������ع������͹    ���
    public float shootInterval ; // ����������ҧ�ѹ�����ҧ����ԧ���Ф���
    public float shootingRange = 5f; // ���зҧ����ѵ������ö�ԧ��������
    public LayerMask playerLayer; // �������ͧ������ (������Ѻ��õ�Ǩ�ͺ��ê�)


    void Start()
    {
        // ��Ǩ�ͺ����������ԧ�ء� shootInterval �Թҷ�
        InvokeRepeating("CheckPlayerInRangeAndShoot", 0f, shootInterval); 
    }

    void CheckPlayerInRangeAndShoot()
    {
        // �Ҵ���� Scene view (Debugging code only visible in Scene view)
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

            // ������ع���仢�ҧ˹�ҵ����ȷҧ�ͧ firePoint
            rb.AddForce(-firePoint.right * bulletForce, ForceMode2D.Impulse);

            // ����� Coroutine ���͵�Ǩ�ͺ��з���¡���ع����Ͷ֧�����٧�ش
            StartCoroutine(DestroyBulletAfterDistance(bullet));
        }
    }

    IEnumerator DestroyBulletAfterDistance(GameObject bullet)
    {
        // �ͨ����ҡ���ع������͹���֧�����٧�ش
        while (bullet != null && Vector2.Distance(firePoint.position, bullet.transform.position) < maxDistance)
        {
            yield return null;
        }

        // ����¡���ع������Թ���з���˹�
        if (bullet != null)
        {
            Destroy(bullet);
        }
    }

    // �ʴ������ԧ� Unity Editor ���������µ�͡�û�Ѻ��
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePoint.position, shootingRange);
    }

}

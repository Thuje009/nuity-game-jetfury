using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��Ǩ�ͺ��� GameObject ������ع���� Tag �� "Eplayer"
        if (collision.gameObject.CompareTag("Player"))
        {
            // ����¡���ع
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��Ǩ�ͺ��� GameObject ������ع���� Tag �� "Eplayer"
        if (collision.gameObject.CompareTag("Eplayer"))
        {
            // ����¡���ع
            Destroy(gameObject);
        }
    }


}

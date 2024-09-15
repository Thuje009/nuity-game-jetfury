using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ตรวจสอบว่า GameObject ที่กระสุนชนมี Tag เป็น "Eplayer"
        if (collision.gameObject.CompareTag("Eplayer"))
        {
            // ทำลายกระสุน
            Destroy(gameObject);
        }
    }


}

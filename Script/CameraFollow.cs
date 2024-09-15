using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera mainCamera;
    public Transform player;  // อ้างอิงตำแหน่งของผู้เล่น
    public Vector3 offset;    // ค่า offset สำหรับตำแหน่งกล้อง

    public float targetSize = 10f;  // ขนาดกล้องเป้าหมายที่จะซูมออกถึง
    public float zoomSpeed = 2f;    // ความเร็วในการซูมออก

    private float initialCameraX;  // ตำแหน่งเริ่มต้นของกล้องในแกน x

    // ตัวแปรสำหรับขอบเขตการเคลื่อนไหวของกล้อง
    public Vector2 minBounds;
    public Vector2 maxBounds;

    void Start()
    {
        // เก็บตำแหน่งเริ่มต้นของกล้องในแกน x
        initialCameraX = transform.position.x;
    }

    void Update()
    {
        // ซูมออกกล้องจนถึงขนาดที่กำหนด
        if (mainCamera.orthographicSize < targetSize)
        {
            mainCamera.orthographicSize += zoomSpeed * Time.deltaTime;
        }
        else
        {
            // คำนวณตำแหน่งเป้าหมายของกล้องในแกน x
            float targetX = Mathf.Max(initialCameraX, player.position.x + offset.x);

            // ตั้งตำแหน่งกล้องในแกน x ให้ไม่ต่ำกว่าตำแหน่งเริ่มต้น
            float clampedX = Mathf.Clamp(targetX, minBounds.x, maxBounds.x);

            // ตั้งตำแหน่งกล้องในแกน y โดยอิงตามตำแหน่งของผู้เล่นและ offset
            float clampedY = Mathf.Clamp(player.position.y + offset.y, minBounds.y, maxBounds.y);

            // กำหนดตำแหน่งกล้องที่ถูกจำกัด
            transform.position = new Vector3(
                clampedX,               // ตำแหน่ง x ของกล้อง
                clampedY,               // ตำแหน่ง y ของกล้อง
                transform.position.z    // ล็อคตำแหน่ง z ของกล้อง
            );
        }
    }
}

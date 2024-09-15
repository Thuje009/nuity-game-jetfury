using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera mainCamera;
    public Transform player;  // ��ҧ�ԧ���˹觢ͧ������
    public Vector3 offset;    // ��� offset ����Ѻ���˹觡��ͧ

    public float targetSize = 10f;  // ��Ҵ���ͧ������·��Ы���͡�֧
    public float zoomSpeed = 2f;    // ��������㹡�ë���͡

    private float initialCameraX;  // ���˹�������鹢ͧ���ͧ�᡹ x

    // ���������Ѻ�ͺࢵ�������͹��Ǣͧ���ͧ
    public Vector2 minBounds;
    public Vector2 maxBounds;

    void Start()
    {
        // �纵��˹�������鹢ͧ���ͧ�᡹ x
        initialCameraX = transform.position.x;
    }

    void Update()
    {
        // ����͡���ͧ���֧��Ҵ����˹�
        if (mainCamera.orthographicSize < targetSize)
        {
            mainCamera.orthographicSize += zoomSpeed * Time.deltaTime;
        }
        else
        {
            // �ӹǳ���˹�������¢ͧ���ͧ�᡹ x
            float targetX = Mathf.Max(initialCameraX, player.position.x + offset.x);

            // ��駵��˹觡��ͧ�᡹ x �������ӡ��ҵ��˹��������
            float clampedX = Mathf.Clamp(targetX, minBounds.x, maxBounds.x);

            // ��駵��˹觡��ͧ�᡹ y ���ԧ������˹觢ͧ��������� offset
            float clampedY = Mathf.Clamp(player.position.y + offset.y, minBounds.y, maxBounds.y);

            // ��˹����˹觡��ͧ���١�ӡѴ
            transform.position = new Vector3(
                clampedX,               // ���˹� x �ͧ���ͧ
                clampedY,               // ���˹� y �ͧ���ͧ
                transform.position.z    // ��ͤ���˹� z �ͧ���ͧ
            );
        }
    }
}

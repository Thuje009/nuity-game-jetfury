using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public float shootingInterval = 1f; // �������������ҧ����ԧ���Ф���

    void Start()
    {
        // ���¡��ѧ��ѹ Shoot �ء� shootingInterval �Թҷշѹ��������������
        InvokeRepeating("Shoot", 0f, shootingInterval);
    }

    void Shoot()
    {
        // ���¡�����ԧ
        animator.SetTrigger("Eshoot");
    }

    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("shoot");
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("shoot"))
        {
            animator.ResetTrigger("shoot");
            animator.SetTrigger("play"); 
        }
    }
}


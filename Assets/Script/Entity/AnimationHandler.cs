using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMovingL = Animator.StringToHash("isMovingL");
    private static readonly int IsMovingR = Animator.StringToHash("isMovingR");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsDamage = Animator.StringToHash("isDamage");
    private static readonly int IsDestroy = Animator.StringToHash("isDestroy");
    protected Animator animator;

    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        bool isMoving = obj.magnitude > .5f;
        if (gameObject.CompareTag("Enemy2"))
        {
            animator.SetBool(IsMoving, isMoving);
        }
        else if(gameObject.CompareTag("Enemy"))
        {
            animator.SetBool(IsMovingL, isMoving);
            animator.SetBool(IsMovingR, isMoving);
        }
        else
        {
            animator.SetBool(IsMoving, isMoving);
            animator.SetBool(IsMovingL, isMoving);
            animator.SetBool(IsMovingR, isMoving);
        }
    }

    public void DestroyProjectile()
    {
        animator.SetBool(IsDestroy, true);
    }

    public void Damage()
    {
        animator.SetBool(IsDamage, true);
    }

    public void InvincibilityEnd()
    {
        animator.SetBool(IsDamage, false);
    }
}

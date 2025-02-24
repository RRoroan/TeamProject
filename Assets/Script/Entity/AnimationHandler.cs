using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMovingL = Animator.StringToHash("isMovingL");
    private static readonly int IsMovingR = Animator.StringToHash("isMovingR");
    protected Animator animator;

    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        bool isMoving = obj.magnitude > .5f;
        animator.SetBool(IsMovingL, isMoving);
        animator.SetBool(IsMovingR, isMoving);
        
    }
}

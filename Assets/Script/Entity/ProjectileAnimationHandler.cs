using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAnimationHandler : MonoBehaviour
{
    protected Animator animator;

    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }


    public void PlayDestroyAnimation()
    {

        if (animator != null)
        {
            animator.speed = 0.5f;
            Debug.Log("Setting Trigger");
            animator.SetTrigger("Explosion"); 
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            Destroy(gameObject); // If no animator is found, destroy immediately
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        float animTime = GetAnimationClipLength("Explosion");

        yield return new WaitForSeconds(animTime);
        Destroy(gameObject);
    }

    private float GetAnimationClipLength(string clipName)
    {
        if (animator.runtimeAnimatorController == null) return 0.5f;

        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }

        return 0.5f;
    }
}

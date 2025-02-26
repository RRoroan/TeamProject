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
            animator.SetTrigger("Destroy");  // Assumes an animation trigger named "Destroy"
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            Destroy(gameObject); // If no animator is found, destroy immediately
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // Get the length of the "ProjectileDestroy" animation
        float animTime = GetAnimationClipLength("ProjectileDestroy");

        // Wait for the animation to finish before destroying
        yield return new WaitForSeconds(animTime);
        Destroy(gameObject);
    }

    private float GetAnimationClipLength(string clipName)
    {
        if (animator.runtimeAnimatorController == null) return 0.5f; // Default delay

        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }

        return 0.5f; // Default delay if clip not found
    }
}

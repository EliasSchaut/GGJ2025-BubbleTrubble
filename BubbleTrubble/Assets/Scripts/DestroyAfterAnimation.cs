using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    private void Start()
    {
        // Destroy the object after the animation length
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, animationLength);
        }
    }
}

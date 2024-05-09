using System.Collections;
using UnityEngine;

public class Attack_hero1 : MonoBehaviour
{
    public Animator animator;  // Ensure this is linked to the Animator component in the inspector
    public HeroHit hero;       // Reference to the HeroHit script on your hero object
    public float delay = 0.3f; // Delay before the attack happens

    private bool isReadyToAttack = true; // Flag to control attack readiness

    void Update()
    {
        // Call Attack from Update if needed, or under specific conditions
        if (hero.isAttacking && isReadyToAttack)
        {
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack()
    {
        isReadyToAttack = false; // Set flag to false to prevent reentry
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        animator.SetTrigger("Attack"); // Trigger the attack animation
        Debug.Log("Attacking");

        // Wait for animation to potentially finish or other conditions to reset
        yield return new WaitForSeconds(1f); // Example additional wait time
        isReadyToAttack = true; // Reset the flag after attack completes
    }
}

using System.Collections;
using UnityEngine;

public class HeroHit : MonoBehaviour
{
    public HeroMovement heroMovement;
    private bool isAnotherHeroNearby = false;
    public int attackDamage = 10;
    public bool isAttacking = false;
    public int point = 1;
    public Animator animator;

    private void Awake()
    {
        heroMovement = GetComponent<HeroMovement>();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (isAttacking)
        {
            return;
        }

        if (target.gameObject.CompareTag("Enemy") || target.gameObject.CompareTag("enemy wall"))
        {
            Debug.Log(gameObject.name + " Hit Enemy");
            animator.SetBool("Attacking", true);
            HealthEnemy enemyHealth = target.gameObject.GetComponent<HealthEnemy>();
            if (enemyHealth != null)
            {
                Debug.Log(gameObject.name + " Speed  hero 0");
                heroMovement.StopMovement();
                StartCoroutine(AttackEnemy(enemyHealth));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(gameObject.name + " Another hero left, resuming.");
        isAnotherHeroNearby = false;
        heroMovement.StartMovement(30.0f);
    }

    private IEnumerator AttackEnemy(HealthEnemy enemyHealth)
    {
        isAttacking = true;

        while (enemyHealth.currentHealth > 0)
        {
            enemyHealth.TakeDamage(attackDamage);
            yield return new WaitForSeconds(1f);
        }

        isAttacking = false;
        animator.SetBool("Attacking", false);
        heroMovement.StartMovement(30.0f);
    }
}

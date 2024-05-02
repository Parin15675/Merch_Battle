using System.Collections;
using UnityEngine;

public class HeroHit : MonoBehaviour
{
    private HeroMovement heroMovement;
    public int attackDamage = 10;
    public bool isAttacking = false;

    private void Awake()
    {
        heroMovement = GetComponent<HeroMovement>();
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
            Health enemyHealth = target.gameObject.GetComponent<Health>();

            if (heroMovement != null && enemyHealth != null)
            {
                Debug.Log("Speed 0");
                heroMovement.StopMovement();
                StartCoroutine(AttackEnemy(enemyHealth));
            }

        }
        else if (target.gameObject.CompareTag("Hero"))
        {
            Debug.Log("met hero");
            if (heroMovement != null)
            {
                Debug.Log("Speed 0");
                heroMovement.StopMovement();
            }
        }
        else
        {
            Debug.Log("Hit something else");
        }
    }

    private IEnumerator AttackEnemy(Health enemyHealth)
    {
        isAttacking = true;

        while (enemyHealth.currentHealth > 0)
        {
            enemyHealth.TakeDamage(attackDamage);
            yield return new WaitForSeconds(1f); 
        }

        isAttacking = false;
        heroMovement.StartMovement(30.0f); 
    }
}

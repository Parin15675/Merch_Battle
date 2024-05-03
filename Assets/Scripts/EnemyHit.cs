using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyHit : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    public int attackDamage = 10;
    public bool isAttacking = false;




    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }


    private void OnCollisionEnter2D(Collision2D target)
    {
        Debug.Log("Collision with: " + target.gameObject.name);
        
        if (target.gameObject.CompareTag("Hero") )
        {
            Health heroHealth = target.gameObject.GetComponent<Health>();
            Debug.Log("Hit Hero");
            
            if (enemyMovement != null && heroHealth != null)
            {
                Debug.Log("Enemy speed 0");
                enemyMovement.StopMovement();
                StartCoroutine(AttackEnemy(heroHealth));
            }
        }
        else if (target.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("met enemy01");
            if (enemyMovement != null)
            {
                Debug.Log("Speed 000");
                enemyMovement.StopMovement();

            }
        }
        else
        {
            Debug.Log("Hit something else");
        }
    }

    private void OnCollisionExit2D(Collision2D target)
    {
        if (!target.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy has left, resuming movement.");
            enemyMovement.StartMovement(-30.0f); 
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
        enemyMovement.StartMovement(-30.0f); 
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyHit : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    private bool isAnotherEnemyNearby = false;
    public int attackDamage = 10;
    public bool isAttacking = false;
    public Animator animator;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        Debug.Log("Collision with: " + target.gameObject.name);

        RectTransform thisRect = this.gameObject.GetComponent<RectTransform>();
        RectTransform rect = target.gameObject.GetComponent<RectTransform>();

        if (target.gameObject.CompareTag("Hero"))
        {
            Health heroHealth = target.gameObject.GetComponent<Health>();
            Debug.Log(gameObject.name + "Hit Hero");

            if (enemyMovement != null && heroHealth != null)
            {
                Debug.Log(gameObject.name + "Enemy speed 0");
                enemyMovement.StopMovement();
                
                StartCoroutine(AttackEnemy(heroHealth));
            }
        }
        else if(target.gameObject.CompareTag("player castle"))
        {
            Health castleHealth = target.gameObject.GetComponent<Health>();
            Debug.Log(gameObject.name + "Hit castle");

            if (castleHealth != null)
            {
                Debug.Log(gameObject.name + "Enemy speed 0");
                enemyMovement.StopMovement();
                
                StartCoroutine(AttackEnemy(castleHealth));
            }
        }
        else if (target.gameObject.CompareTag("Human arrow"))
        {
            enemyMovement.speed = enemyMovement.speed / 2;
            
            
            //if (target.gameObject.CompareTag("Human"))
            //{
            //    Health heroHealth = target.gameObject.GetComponent<Health>();
            //    Debug.Log(gameObject.name + "Hit Hero");

            //    if (enemyMovement != null && heroHealth != null)
            //    {
            //        Debug.Log(gameObject.name + "Enemy speed 0");
            //        enemyMovement.StopMovement();

            //        StartCoroutine(AttackEnemy(heroHealth));
            //    }
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Another hero left, resuming.");
        isAnotherEnemyNearby = false;
        enemyMovement.StartMovement(-30.0f);
    }

    private IEnumerator AttackEnemy(Health enemyHealth)
    {
        isAttacking = true;
        animator.SetBool("Attacking", true);

        while (enemyHealth.currentHealth > 0)
        {
            enemyHealth.TakeDamage(attackDamage);
            yield return new WaitForSeconds(1f); 
        }

        animator.SetBool("Attacking", false);

        isAttacking = false;
        enemyMovement.StartMovement(-30.0f); 
    }







}

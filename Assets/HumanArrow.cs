using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using UnityEngine.UIElements;

public class HumanArrow : MonoBehaviour
{
    public HeroMovement heroMovement;
    private bool isAnotherHeroNearby = false;
    public int attackDamage = 10;
    public bool isAttacking = false;
    public int point = 1;
    public Animator animator;
    public Transform arrowSpawnPoint;
    public GameObject arrowprefab;

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Enemy") || target.gameObject.CompareTag("enemy wall"))
        {
            Debug.Log(gameObject.name + "Hit Enemy by arrow");
            animator.SetBool("Attacking", true);
            HealthEnemy enemyHealth = target.gameObject.GetComponent<HealthEnemy>();
            Debug.Log(gameObject.name + "Speed  hero 0");
            heroMovement.StopMovement();
            StartCoroutine(AttackEnemy(enemyHealth));
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Hero"))
        //{
        Debug.Log(gameObject.name + "Another hero left, resuming.");
        isAnotherHeroNearby = false;
        heroMovement.StartMovement(30.0f);
        //}
    }

    private IEnumerator AttackEnemy(HealthEnemy enemyHealth)
    {
        isAttacking = true;

        while (enemyHealth.currentHealth > 0)
        {
            Instantiate(arrowprefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            enemyHealth.TakeDamage(attackDamage);
            yield return new WaitForSeconds(1f);
        }

        isAttacking = false;
        animator.SetBool("Attacking", false);
        heroMovement.StartMovement(30.0f);
    }
}

using System.Collections;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private BaseCharacter baseCharacter;
    private EnemyMovement enemyMovement;

    public int attackDamage;
    public bool isAttacking = false;
    public Animator animator;

    private void Awake()
    {
        baseCharacter = GetComponent<BaseCharacter>();
        enemyMovement = GetComponent<EnemyMovement>();
        attackDamage = baseCharacter.attack;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (isAttacking)
        {
            return; 
        }

        if (target.GetType() == typeof(BoxCollider2D))
        {
            if (target.CompareTag("Hero"))
            {
                Health targetHealth = target.GetComponent<Health>();
                if (targetHealth != null)
                {
                    enemyMovement.StopMovement();
                    StartCoroutine(AttackTarget(targetHealth));
                }
            }
        }
    }

    private IEnumerator AttackTarget(Health targetHealth)
    {
        isAttacking = true;
        animator.SetBool("Attacking", true);

        while (targetHealth.currentHealth > 0)
        {
            targetHealth.TakeDamage(attackDamage);
            yield return new WaitForSeconds(1f);
        }

        animator.SetBool("Attacking", false);
        isAttacking = false;
        enemyMovement.StartMovement();
    }
}

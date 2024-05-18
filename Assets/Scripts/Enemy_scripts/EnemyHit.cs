using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private BaseCharacter baseCharacter;
    private EnemyMovement enemyMovement;

    public int attackDamage;
    public bool isAttacking = false;
    public Animator animator;

    // Dictionary to track the number of enemies attacking each target
    private static Dictionary<Health, int> targetAttackers = new Dictionary<Health, int>();

    AudioManeger audioManeger;

    private void Awake()
    {
        baseCharacter = GetComponent<BaseCharacter>();
        enemyMovement = GetComponent<EnemyMovement>();
        attackDamage = baseCharacter.attack;
        audioManeger = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManeger>();
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
                    // Check if the target already has two attackers
                    if (!targetAttackers.ContainsKey(targetHealth) || targetAttackers[targetHealth] < 2)
                    {
                        // Increment the number of attackers for this target
                        if (!targetAttackers.ContainsKey(targetHealth))
                        {
                            targetAttackers[targetHealth] = 0;
                        }
                        targetAttackers[targetHealth]++;

                        enemyMovement.StopMovement();
                        StartCoroutine(AttackTarget(targetHealth));
                    }
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
            audioManeger.PlaySFX(audioManeger.Undead_atk);
            yield return new WaitForSeconds(1f);
        }

        animator.SetBool("Attacking", false);
        isAttacking = false;
        enemyMovement.WalkForward();

        // Decrement the number of attackers for this target
        if (targetAttackers.ContainsKey(targetHealth))
        {
            targetAttackers[targetHealth]--;
            if (targetAttackers[targetHealth] <= 0)
            {
                targetAttackers.Remove(targetHealth);
            }
        }
    }
}

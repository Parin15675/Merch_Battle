using System.Collections;
using UnityEngine;

public class HeroHit : MonoBehaviour
{
    private BaseCharacter baseCharacter;
    private HeroMovement heroMovement;

    public int attackDamage;
    public bool isAttacking = false;
    public int point = 1;
    public Animator animator;

    AudioManeger audioManeger;

    private void Awake()
    {
        baseCharacter = GetComponent<BaseCharacter>();
        heroMovement = GetComponent<HeroMovement>();
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
            if (target.gameObject.CompareTag("Enemy"))
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

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(gameObject.name + " Another hero left, resuming.");
        heroMovement.WalkForward();
    }

    private IEnumerator AttackEnemy(HealthEnemy enemyHealth)
    {
        isAttacking = true;

        while (enemyHealth.currentHealth > 0)
        {
            enemyHealth.TakeDamage(attackDamage);
            audioManeger.PlaySFX(audioManeger.Human_atk);
            yield return new WaitForSeconds(1f);
        }

        isAttacking = false;
        animator.SetBool("Attacking", false);
        heroMovement.WalkForward();
    }
}

using System.Collections;
using UnityEngine;

public class HeroHit : MonoBehaviour
{
    private HeroMovement heroMovement;
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
        if (target.gameObject.CompareTag("Enemy") || target.gameObject.CompareTag("enemy wall"))
        {
            Debug.Log(gameObject.name + "Hit Enemy");
            animator.SetBool("Attackig", true);
            HealthEnemy enemyHealth = target.gameObject.GetComponent<HealthEnemy>();
            Debug.Log(gameObject.name + "Speed  hero 0");
            heroMovement.StopMovement();
            StartCoroutine(AttackEnemy(enemyHealth));


        }
    }

    //private void OnCollisionEnter2D(Collision2D target)
    //{
    //    if (target.gameObject.CompareTag("Enemy"))
    //    {
    //        Debug.Log(gameObject.name + "Hit Enemy");
    //        Health enemyHealth = target.gameObject.GetComponent<Health>();
    //        Debug.Log(gameObject.name + "Speed  hero 0");
    //        heroMovement.StopMovement();
    //        StartCoroutine(AttackEnemy(enemyHealth));
            

    //    }
    //    //else if (target.gameObject.CompareTag("Hero"))
    //    //{
    //    //    RectTransform thisRect = this.gameObject.GetComponent<RectTransform>();
    //    //    RectTransform rect = target.gameObject.GetComponent<RectTransform>();
    //    //    Debug.Log(rect.transform.localPosition.y + " and " + thisRect.transform.localPosition.y);

    //    //    Debug.Log(gameObject.name + "met hero");
    //    //    if (!(thisRect.transform.localPosition.y > rect.transform.localPosition.y) || (thisRect.transform.localPosition.x < rect.transform.localPosition.x))
    //    //    {
    //    //        Debug.Log(gameObject.name + "Met another hero, stopping.");
    //    //        isAnotherHeroNearby = true;
    //    //        heroMovement.StopMovement();
    //    //    }
    //    //}
    //}

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
            enemyHealth.TakeDamage(attackDamage);
            yield return new WaitForSeconds(1f); 
        }

        isAttacking = false;
        animator.SetBool("Attackig", false);
        heroMovement.StartMovement(30.0f); 
    }
}

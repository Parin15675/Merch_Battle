using System.Collections;
using UnityEngine;

public class HeroHit : MonoBehaviour
{
    private HeroMovement heroMovement;
    private bool isAnotherHeroNearby = false;
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
            Debug.Log("Speed  hero 0");
            heroMovement.StopMovement();
            StartCoroutine(AttackEnemy(enemyHealth));
            

        }
        else if (target.gameObject.CompareTag("Hero"))
        {
            RectTransform thisRect = this.gameObject.GetComponent<RectTransform>();
            RectTransform rect = target.gameObject.GetComponent<RectTransform>();
            Debug.Log(rect.transform.localPosition.y + " and " + thisRect.transform.localPosition.y);

            Debug.Log("met hero");
            if (!(thisRect.transform.localPosition.y > rect.transform.localPosition.y))
            {
                Debug.Log("Met another hero, stopping.");
                isAnotherHeroNearby = true;
                heroMovement.StopMovement();
            }
        }
        else
        {
            Debug.Log("Hit something else");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            Debug.Log("Another hero left, resuming.");
            isAnotherHeroNearby = false;
            heroMovement.StartMovement(30.0f);
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

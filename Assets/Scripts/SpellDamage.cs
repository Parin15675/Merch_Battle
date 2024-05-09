using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamage : MonoBehaviour
{
    public int damage = 10;

    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(gameObject.name + "Hit Enemy");
            HealthEnemy enemyHealth = target.gameObject.GetComponent<HealthEnemy>();
            StartCoroutine(AttackEnemy(enemyHealth));
        }
    }

    private IEnumerator AttackEnemy(HealthEnemy enemyHealth)
    {

        while (enemyHealth.currentHealth > 0)
        {
            enemyHealth.TakeDamage(damage);
            yield return new WaitForSeconds(1f);
        }
        
    }
}

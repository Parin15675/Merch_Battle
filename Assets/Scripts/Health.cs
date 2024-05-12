using UnityEngine;
using System.Collections;  

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int point = 1;

    public HealthBar healthBar;
    public TextUpdater text;
    public Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " takes " + damage + " damage.");
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
        Debug.Log(gameObject.name + " healed " + amount + " health.");
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died.");
        animator.SetBool("Die", true);

        StartCoroutine(DelayedDestruction());
    }

    private IEnumerator DelayedDestruction()
    {
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}

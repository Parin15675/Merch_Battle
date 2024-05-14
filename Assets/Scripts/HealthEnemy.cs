using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public TextUpdater text;
    public Animator animator;

    void Start()
    {
        text = GameObject.Find("dieCount").gameObject.GetComponent<TextUpdater>(); 
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
            Die_enemy();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log(gameObject.name + " healed " + amount + " health.");
    }

    void Die_enemy()
    {
        text = GameObject.Find("dieCount").gameObject.GetComponent<TextUpdater>();
        text.dieCount--;
        Debug.Log("die count");
        Debug.Log(gameObject.name + " died.1234");
        animator.SetBool("Die", true);

        StartCoroutine(DelayedDestruction());

    }

    private IEnumerator DelayedDestruction()
    {
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}

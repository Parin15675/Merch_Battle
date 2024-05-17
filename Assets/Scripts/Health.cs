using UnityEngine;
using System.Collections;  

public class Health : MonoBehaviour
{
    private BaseCharacter baseCharacter;

    public int maxHealth;
    public int currentHealth;
    public int point = 1;

    public HealthBar healthBar;
    public TextUpdater text;
    public Animator animator;

    AudioManeger audioManeger;

    private void Awake()
    {
        baseCharacter = GetComponent<BaseCharacter>();
        maxHealth = baseCharacter.health;
        audioManeger = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManeger>();
    }

    private void Start()
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
        audioManeger.PlaySFX(audioManeger.Human_dead);

        StartCoroutine(DelayedDestruction());
    }

    private IEnumerator DelayedDestruction()
    {
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}

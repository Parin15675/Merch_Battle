using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    private BaseCharacter baseCharacter;
    private EnemyMovement enemyMovement;
    private EnemyHit enemyHit;
    private RangeEnemyAttack rangeEnemyAttack;

    public int maxHealth;
    public int currentHealth;

    public HealthBar healthBar;
    public TextUpdater text;
    public Animator animator;

    [SerializeField] private GameObject damagePopupPrefab;

    AudioManeger audioManeger;

    private void Awake()
    {
        baseCharacter = GetComponent<BaseCharacter>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyHit = GetComponent<EnemyHit>();
        rangeEnemyAttack = GetComponent<RangeEnemyAttack>();
        maxHealth = baseCharacter.health;
        audioManeger = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManeger>();
    }

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
            Die_enemy();
        } else
        {
            InstantiateDamagePopup(damage);
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
        enemyMovement.enabled = false; 
        if (enemyHit != null)
        {
            enemyHit.enabled = false;
        } else
        {
            rangeEnemyAttack.enabled = false;
        }
        
        CoinsManager.Instance.AddCoins(3);
        Debug.Log(gameObject.name + " died.");
        animator.SetBool("Die", true);
        audioManeger.PlaySFX(audioManeger.Undead_dead);

        StartCoroutine(DelayedDestruction());
    }

    private IEnumerator DelayedDestruction()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void InstantiateDamagePopup(int damage)
    {
        GameObject damagePopup = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity);
        damagePopup.transform.SetParent(transform); // Set the parent without changing the world position
        TextMeshProUGUI textMeshPro = damagePopup.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = damage.ToString();

        Destroy(damagePopup, 0.5f);
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    private BaseCharacter baseCharacter;

    public int maxHealth;
    public int currentHealth;

    public HealthBar healthBar;
    public TextUpdater text;
    public Animator animator;

    [SerializeField] private GameObject damagePopupPrefab;
    [SerializeField] private Transform spawner; // Reference to the spawner GameObject

    AudioManeger audioManeger;

    private void Awake()
    {
        baseCharacter = GetComponent<BaseCharacter>();
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

        ShowDamagePopup(damage); // Show damage popup on every hit

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

    private void ShowDamagePopup(int damage)
    {
        // Instantiate the damage popup prefab as a child of the spawner
        GameObject popup = Instantiate(damagePopupPrefab, spawner.position, Quaternion.identity);

        // Set the local position relative to the spawner
        popup.transform.localPosition = new Vector3(0, 1, 0);

        TextMeshProUGUI tmp = popup.GetComponentInChildren<TextMeshProUGUI>();
        if (tmp != null)
        {
            tmp.text = damage.ToString();
        }

        Animator popupAnimator = popup.GetComponent<Animator>();
        if (popupAnimator != null)
        {
            // Optionally, you can control the animation state or duration here
            Destroy(popup, popupAnimator.GetCurrentAnimatorStateInfo(0).length);
        }
        else
        {
            // Destroy the popup after a default duration if no animator is present
            Destroy(popup, 1.0f);
        }
    }
}

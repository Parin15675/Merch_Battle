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
    public GameObject text_popup;

    private void Awake()
    {
        baseCharacter = GetComponent<BaseCharacter>();
        maxHealth = baseCharacter.health;
    }

    void Start()
    {
        text = GameObject.Find("dieCount").gameObject.GetComponent<TextUpdater>(); 
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F10)) 
    //    {
    //        CreatePopUp(Vector3.one, Random.Range(0,1000).ToString());
    //    }
    //}

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

    //public void CreatePopUp(Vector3 position, string text)
    //{
    //    var popup = Instantiate(text_popup, position , Quaternion.identity);
    //    popup.transform.SetParent(transform);
    //    var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    //    temp.text = text;

    //    Destroy(popup,1f);

    //}
}

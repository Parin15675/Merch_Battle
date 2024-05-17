using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireballAbility : Ability
{
    private float lastYPos;
    public int damage = 10;

    AudioManeger audioManeger;

    private void Awake()
    {
        audioManeger = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManeger>();
    }

    private void modifyStartPosition(Vector3 parentPos)
    {
        lastYPos = parentPos.y;
        gameObject.transform.position = new Vector3(parentPos.x, parentPos.y + 1000, 1);
    }

    private void disableColliderBeforeHit()
    {
        if (gameObject.transform.position.y > lastYPos + 150)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
        } else
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }

    private void Update()
    {
        disableColliderBeforeHit();
        if (gameObject.transform.position.y < lastYPos)
        {
            gameObject.SetActive(false);
        }
    }

    public override void Activate(GameObject parent)
    {
        transform.SetParent(parent.transform);
        transform.SetAsLastSibling();
        modifyStartPosition(parent.transform.position);
    }


    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(gameObject.name + "Hit Enemy");
            HealthEnemy enemyHealth = target.gameObject.GetComponent<HealthEnemy>();
            audioManeger.PlaySFX(audioManeger.fireball);
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

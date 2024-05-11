using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealAbility : Ability
{
    public int HealingAmount = 1;

    private void modifyStartPosition(Vector3 parentPos)
    {
        gameObject.transform.position = parentPos;
    }

    private void Update()
    {

    }

    public override void Activate(GameObject parent)
    {
        transform.SetParent(parent.transform);
        transform.SetAsLastSibling();
        modifyStartPosition(parent.transform.position);
    }


    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Hero"))
        {
            Debug.Log(gameObject.name + "Heal");
            Health heroHealth = target.gameObject.GetComponent<Health>();
            StartCoroutine(Healing(heroHealth));
        }
    }

    private IEnumerator Healing(Health heroHealth)
    {

        while (heroHealth.currentHealth > 0)
        {
            heroHealth.Heal(HealingAmount);
            yield return new WaitForSeconds(0.1f);
        }

    }
}

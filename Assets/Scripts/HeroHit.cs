using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHit : MonoBehaviour
{
    private HeroMovement heroMovement;

    private void Start()
    {
        // Get the HeroMovement component from the same GameObject
        heroMovement = GetComponent<HeroMovement>();
        if (heroMovement == null)
        {
            Debug.LogError("HeroMovement component not found on " + gameObject.name);
        }
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        Debug.Log("Collision with: " + target.gameObject.name);
        if (target.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
            //TODO attack enemy
            if (heroMovement != null)
            {
                Debug.Log("speed 0");
                heroMovement.StopMovement();
            }
        }
        else if(target.gameObject.CompareTag("Hero"))
        {
            
            if (heroMovement != null)
            {
                Debug.Log("speed 0");
                heroMovement.StopMovement();
            }
        }
        else
        {
            Debug.Log("Hit something else");
        }
    }
}

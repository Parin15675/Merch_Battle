using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
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
        if (target.gameObject.CompareTag("Hero") || target.gameObject.CompareTag("Enemy"))
        {


            Debug.Log("Hit Hero");
            // Call StopMovement if the collided object is an Enemy
            if (heroMovement != null)
            {
                Debug.Log("Enemy speed 0");
                heroMovement.StopMovement();
            }
        }
        else
        {
            Debug.Log("Hit something else");
        }
    }
}

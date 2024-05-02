using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public float speed = 2.0f; // Speed at which the hero should move
    private bool canMove = true; // Flag to control movement

    void Update()
    {
        if (canMove)
        {
            
            float moveAmount = speed * Time.deltaTime; 
            transform.Translate(new Vector3(moveAmount, 0, 0)); 
        }
        else
        {
            
            
            transform.Translate(new Vector3(0, 0, 0)); 
        }
    }

    // Public method to stop the hero
    public void StopMovement()
    {
        canMove = false;
        speed = 0;
    }
}

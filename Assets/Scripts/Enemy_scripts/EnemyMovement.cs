using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float speed = -30.0f; // Speed at which the hero should move
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

    public void StartMovement(float newSpeed)
    {
        speed = -30.0f; // Set new speed if needed
        canMove = true;
        Debug.Log("Movement restarted at speed: " + newSpeed);
    }

}


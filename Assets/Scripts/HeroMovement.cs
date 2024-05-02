using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public float speed = 2.0f; 

    void Update()
    {
        
        float moveAmount = speed * Time.deltaTime; 
        transform.Translate(new Vector3(moveAmount, 0, 0));
    }
}

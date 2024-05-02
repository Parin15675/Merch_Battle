using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMLeft : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    public float speed = -2.0f; // Speed at which the hero should move left

    void Update()
    {
        // Automatically move the hero to the left at a constant speed
        // The speed variable is negative, indicating leftward movement
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}

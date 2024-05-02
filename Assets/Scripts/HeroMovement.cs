using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    public float speed = -2.0f; // Speed at which the hero should move

    void Update()
    {
        // This will automatically move the hero at a constant speed
        rb.velocity = new Vector3(speed, rb.velocity.y, 0f);

    }
}

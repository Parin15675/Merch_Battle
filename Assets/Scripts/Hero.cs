using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
        }
        else
        {
            Debug.Log("Hit2");
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public Animator animator;  
    public HeroMovement hero;       
    public float delay = 0.3f; 

    void Update()
    {
        
        if (hero.tagged_hero)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Speed_bar : MonoBehaviour
{
    public HeroMovement hero_movement;
    public Slider slider;

    private void Start()
    {
        slider.value = hero_movement.point;
    }

    public void update_spd()
    {

        if (CoinsManager.coins > 0)
        {
            if (hero_movement.point < 10)
            {
                hero_movement.speed += 2.0f;
                hero_movement.point += 1;
                CoinsManager.coins -= 1;
                slider.value = hero_movement.point;
            }
        }
    }
}

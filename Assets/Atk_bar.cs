using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Atk_bar : MonoBehaviour
{
    public HeroHit hero_hit;
    public Slider slider;

    private void Start()
    {
        slider.value = hero_hit.point;
    }

    public void update_atk()
    {
        if (hero_hit.point < 10)
        {
            hero_hit.attackDamage += 10;
            hero_hit.point += 1;
            slider.value = hero_hit.point;
        }
        else
        {
            Debug.Log("level max");
        }

    }
}

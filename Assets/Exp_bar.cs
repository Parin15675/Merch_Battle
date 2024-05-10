using UnityEngine;
using UnityEngine.UI;

public class Exp_bar : MonoBehaviour
{
    public Health hero_health;
    public Slider slider;

    private void Start()
    {
        slider.value = hero_health.point;
    }

    public void update_hp()
    {
        if (hero_health.point < 10)
        {
            hero_health.maxHealth += 10;
            hero_health.currentHealth += 10;
            hero_health.point += 1;
            slider.value = hero_health.point;
        }

    }
}

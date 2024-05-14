using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUp_undead : MonoBehaviour
{
    public TextMeshProUGUI textHP;
    public TextMeshProUGUI textATK;
    public TextMeshProUGUI textSPD;
    public HealthEnemy undead_health;
    public EnemyHit undead_atk;
    public EnemyMovement undead_movement;

    void Update()
    {
        UpdateText_HP();
        UpdateText_ATK();
        UpdateText_SPD();
    }


    public void UpdateText_HP()
    {
        textHP.text = undead_health.maxHealth.ToString();
    }

    public void UpdateText_ATK()
    {
        textATK.text = undead_atk.attackDamage.ToString();
    }

    public void UpdateText_SPD()
    {
        float speed = (float)(undead_movement.speed * -1.0);

        textSPD.text = speed.ToString();
    }
}

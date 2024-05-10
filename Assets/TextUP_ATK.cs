using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUP_ATK : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public HeroHit hero_hit;

    void Update()
    {
        UpdateText();
    }


    public void UpdateText()
    {
        textMesh.text = hero_hit.attackDamage.ToString();
    }
}

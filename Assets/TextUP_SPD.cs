using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUP_SPD : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public HeroMovement hero_movement;

    void Update()
    {
        UpdateText();
    }


    public void UpdateText()
    {
        textMesh.text = hero_movement.speed.ToString();
    }
}

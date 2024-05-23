using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUP_HP : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public Health hero_health;

    void Update()
    {
        UpdateText();
    }


    public void UpdateText()
    {
        textMesh.text = hero_health.maxHealth.ToString();
    }

}

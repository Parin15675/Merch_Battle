using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsManeger : MonoBehaviour
{
    public static int coins = 5;
    public TextMeshProUGUI textMesh;

    private void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
         textMesh.text = coins.ToString();
    }
}

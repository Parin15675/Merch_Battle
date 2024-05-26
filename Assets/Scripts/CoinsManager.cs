using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager Instance { get; private set; }

    public static int coins = 5;
    public TextMeshProUGUI textMesh;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keeps the instance alive across scenes
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }

    private void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        textMesh.text = coins.ToString();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateText();
    }
}

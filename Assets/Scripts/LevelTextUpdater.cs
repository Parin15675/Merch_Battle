using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTextUpdater : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public TextMeshProUGUI textMesh;
    private int number;

    void Awake()
    {
        number = gameManager.level;
    }

    void Update()
    {
        UpdateText();
    }


    public void UpdateText()
    {
        textMesh.text = "Level " + number.ToString();
    }

}


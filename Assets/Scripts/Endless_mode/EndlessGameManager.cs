using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EndlessGameManager : MonoBehaviour
{
    private static int currentWave = 1;
    public static int GetCurrentWave() { return currentWave; }

    private bool isAdd = false;

    public TextMeshProUGUI textMesh;
    public GameObject playerCastle;
    public GameObject enemyCastle;
    public WaveEndScreen waveEndScreen;
    public StatUpgrade statUpgradeScreen;
    public TileBoard board;

    private void Awake()
    {
    }

    private void Start()
    {
        if (textMesh != null)
            textMesh.text = "Wave " + currentWave;  

        NewGame();
    }

    private void Update()
    {
        if (playerCastle == null)
        {
            Debug.Log("Game over");
            GameOver();
        }

        if (enemyCastle == null)
        {
            Debug.Log("Success");
            Success();
        }

    }

    public void NewGame()
    {
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void GameOver()
    {
        waveEndScreen.Setup();
        TextMeshProUGUI panelText = waveEndScreen.GetComponentInChildren<TextMeshProUGUI>();
        panelText.text = "You Lose";
        TextMeshProUGUI buttonText = waveEndScreen.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "Restart";
        board.enabled = false;
    }

    public void Success()
    {
        if((currentWave - 1) % 3 == 0)
        {
            statUpgradeScreen.Setup();
        }
        else
        {
            waveEndScreen.Setup();
        }
        
        board.enabled = false;
        TextMeshProUGUI panelText = waveEndScreen.GetComponentInChildren<TextMeshProUGUI>();
        panelText.text = "You Win";
        TextMeshProUGUI buttonText = waveEndScreen.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "Continue";

        if (!isAdd)
        {
            currentWave += 1;
            isAdd = true;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndlessGameManager : MonoBehaviour
{
    private static int currentWave = 1;
    public static int GetCurrentWave() { return currentWave; }

    public TextMeshProUGUI textMesh;
    public GameObject playerCastle;
    public GameObject enemyCastle;
    public WaveEndScreen waveEndScreen;
    public TileBoard board;

    public bool hasGameEnded = false;

    private void Awake()
    {
    }

    private void Start()
    {
        hasGameEnded = false;

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
        hasGameEnded = true;
    }

    public void Success()
    {

        waveEndScreen.Setup();
        board.enabled = false;
        TextMeshProUGUI panelText = waveEndScreen.GetComponentInChildren<TextMeshProUGUI>();
        panelText.text = "You Win";
        TextMeshProUGUI buttonText = waveEndScreen.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "Continue";

        if (!hasGameEnded)
        {
            currentWave += 1;
            hasGameEnded = true;
        }
        else
        {
            Debug.Log("hasGameEnded false");
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static int currentGamelevel = 0;
    public static int GetCurrentGamelevel() { return currentGamelevel; }

    public int level = 0;
    public TextMeshProUGUI textMesh;
    public GameObject playerCastle;
    public GameObject enemyCastle;
    public GameOverScreen GameOverScreen;
    public TileBoard board;
    public GameSuccess GameSuccess;

    public bool hasGameEnded = false;

    private void Awake()
    {
        currentGamelevel += 1;
        level = currentGamelevel;
    }

    private void Start()
    {
        textMesh.text = "Level " + level;
        NewGame();
    }

    private void Update()
    {

            if (playerCastle == null)
            {
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
        hasGameEnded = false;
    }

    public void GameOver()
    {
        GameOverScreen.Setup();
        board.enabled = false;
        hasGameEnded = true;
    }

    public void Success()
    {
        if (!hasGameEnded)
        {
            CoinsManeger.coins += 5;
            Debug.Log(CoinsManeger.coins);
        }

        GameSuccess.Setup();
        board.enabled = false;
        hasGameEnded = true;
    }
}

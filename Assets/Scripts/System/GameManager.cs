using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static int currentGamelevel = 1;
    private static int levelPlayed = 0;
    public static int GetCurrentGamelevel() { return currentGamelevel;}
    public static void setCurrentGamelevel(int level) { currentGamelevel = level; }

    public TextMeshProUGUI textMesh;
    public GameObject playerCastle;
    public GameObject enemyCastle;
    public GameOverScreen GameOverScreen;
    public TileBoard board;
    public GameSuccess GameSuccess;

    public bool isAdd = false;
    public bool hasGameEnded = false;

    private void Awake()
    {

    }

    private void Start()
    {

        if (textMesh != null)
            textMesh.text = "Level" + currentGamelevel;

        NewGame();
    }

    private void FixedUpdate()
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
        GameOverScreen.Setup();
        board.enabled = false;
        hasGameEnded = true;
    }

    public void Success()
    {
        
        if (levelPlayed < currentGamelevel)
        {
            levelPlayed = currentGamelevel;
            CoinsManeger.coins += 5;
            Debug.Log(CoinsManeger.coins);
        }

        GameSuccess.Setup();
        board.enabled = false;

        if (!isAdd)
        {
            Debug.Log("currentGamelevel += 1");
            currentGamelevel += 1;
            isAdd = true;
        }

    }
}

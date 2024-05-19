using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static int currentGamelevel = 1;
    public static int GetCurrentGamelevel() { return currentGamelevel; }

    public int level = 1;
    public TextMeshProUGUI textMesh;
    public GameObject playerCastle;
    public GameObject enemyCastle;
    public GameOverScreen GameOverScreen;
    public TileBoard board;
    public GameSuccess GameSuccess;

    public bool hasGameEnded = false;

    private void Awake()
    {
        

        level = currentGamelevel;
    }

    private void Start()
    {
        hasGameEnded = false;

        if (LevelMenu.check_level)
        {
            Debug.Log("GM " + EnemySpawner.gameLevel);
            Debug.Log("Gm " + LevelMenu.level_menu);
            if (textMesh != null)
                textMesh.text = textMesh.text + LevelMenu.level_menu;
            hasGameEnded = true;
        }
        else
        {
            if (textMesh != null)
                textMesh.text = textMesh.text + level;

        }

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
        
        if (!LevelMenu.check_level)
        {
            if (!hasGameEnded)
            {
                Debug.Log("currentGamelevel += 1");
                currentGamelevel += 1;
                hasGameEnded = true;
            }
            else
            {
                Debug.Log("hasGameEnded false");
            }
            
        }
        
    }
}

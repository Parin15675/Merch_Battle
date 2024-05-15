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

    private void Awake()
    {
        currentGamelevel += 1;
        level = currentGamelevel;
    }

    private void Start()
    {
        if (textMesh != null)
            textMesh.text = textMesh.text + level;
        NewGame();
    }

    public void Update()
    {
        if (playerCastle == null)
        {
            this.GameOver();
        }
            
        if(enemyCastle == null)
        {
            this.Success();
        }

        Level_variables.level = level;
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
    }

    public void Success()
    {
        Level_variables.level += 1;
        GameSuccess.Setup(); 
        board.enabled = false;
    }

}

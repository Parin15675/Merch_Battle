using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public GameObject playerCastle;
    public GameObject enemyCastle;
    public GameOverScreen GameOverScreen;
    public TileBoard board;
    public GameSuccess GameSuccess;

    private void Start()
    {
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
        GameSuccess.Setup(); 
        board.enabled = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public GameObject playerCastle;
    public GameOverScreen GameOverScreen;
    public TileBoard board;

    private void Start()
    {
        NewGame();
    }

    public void Update()
    {
        if (playerCastle == null)
            this.GameOver();
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

}

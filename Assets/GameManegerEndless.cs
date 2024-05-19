using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManegerEndless : MonoBehaviour
{
    //private static int currentGameLevel = 0;
    //public static int GetCurrentGameLevel() { return currentGameLevel; }

    //public int level = 0;
    //public TextMeshProUGUI textMesh;
    //public GameObject playerCastle;
    //public GameObject enemyCastle;
    //public GameOverScreen gameOverScreen;
    //public GameSuccess gameSuccess;
    //public TileBoard board;
    //public EnemySpawnerForEndless enemySpawner;

    //public bool hasGameEnded = false;

    //private void Awake()
    //{
    //    currentGameLevel += 1;
    //    level = currentGameLevel;
    //}

    //private void Start()
    //{
    //    if (textMesh != null)
    //        textMesh.text = textMesh.text + level;
    //    NewGame();
    //}

    //private void Update()
    //{
    //    if (playerCastle == null)
    //    {
    //        Debug.Log("Game over");
    //        GameOver();
    //    }

    //    if (enemyCastle == null)
    //    {
    //        Debug.Log("Success");
    //        Success();
    //    }

    //    if (enemySpawner.check_con && !hasGameEnded)
    //    {
    //        Debug.Log("Continuing to next wave");
    //        enemySpawner.check_con = false;
    //        enemySpawner.Con_screen.SetActive(false);
    //        enemySpawner.GenerateWave();
    //    }
    //}

    //public void NewGame()
    //{
    //    board.ClearBoard();
    //    board.CreateTile();
    //    board.CreateTile();
    //    board.enabled = true;
    //    hasGameEnded = false;
    //    enemySpawner.GenerateWave();
    //}

    //public void GameOver()
    //{
    //    gameOverScreen.Setup();
    //    board.enabled = false;
    //    enemySpawner.enabled = false;
    //    hasGameEnded = true;
    //}

    //public void Success()
    //{
    
    //    gameSuccess.Setup();
    //    board.enabled = false;
    //    enemySpawner.enabled = false;
    //    hasGameEnded = true;
    //}
}

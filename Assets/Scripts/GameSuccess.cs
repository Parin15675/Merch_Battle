using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static EnemySpawner;
public class GameSuccess : MonoBehaviour
{
    public int level;

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

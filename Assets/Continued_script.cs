using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continued_script : MonoBehaviour
{

    public EnemySpawnerForEndless spawner;
    public GameObject screen;
    
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
        spawner.check_con = true;
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Town");
    }
}

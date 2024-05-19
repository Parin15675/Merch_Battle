using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continued_script : MonoBehaviour
{

    public EnemySpawnerForEndless spawner;
    public int currentWave  = 0;
    
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
        
        currentWave++;
        SceneManager.LoadScene("EndlessMode");


    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Town");
    }
}

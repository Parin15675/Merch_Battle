using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continued_script : MonoBehaviour
{

    private static int staticWave = 0;
    public static int GetCurrentWave() { return staticWave; }
    public int currentWave = 1;

    private void Awake()
    {
        staticWave++;
        Debug.Log(staticWave);
        currentWave = staticWave;
    }

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
        SceneManager.LoadScene("EndlessMode");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Town");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveEndScreen: MonoBehaviour
{

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void LoadWave()
    {
        SceneManager.LoadScene("EndlessMode");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Town");
    }

}

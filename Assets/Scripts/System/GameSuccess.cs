using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSuccess : MonoBehaviour
{

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
        SceneManager.LoadScene("2048");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Town");
    }

}

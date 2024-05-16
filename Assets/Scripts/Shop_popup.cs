using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop_popup : MonoBehaviour
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
        SceneManager.LoadScene("Main Menu");
    }
}

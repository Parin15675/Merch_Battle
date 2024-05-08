using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("2048");
        
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }
}
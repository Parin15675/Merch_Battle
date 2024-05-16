using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("Town");
        
    }

    public void Level_play()
    {
        SceneManager.LoadScene("2048");

    }

    public void Endless_mode()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("EndlessMode");

    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }

    public void Main_menu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}

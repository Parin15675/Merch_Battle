using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void Play()
    {

        SceneManager.LoadScene("2048");
        
    }

    public void Start_button()
    {

        SceneManager.LoadScene("Town");

    }

    public void Endless_mode()
    {

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    public GameObject[] Level; 
    GameManager gameManager;
    public static bool isPlayedLevel = false;
    public static int level_menu;

    public void Update()
    {
        Debug.Log(GameManager.GetCurrentGamelevel());

        switch (GameManager.GetCurrentGamelevel()) 
        {
            case 1:
                SetLevel(0);
                break;
            case 2:
                SetLevel(1);
                break;
            case 3:
                SetLevel(2); 
                break;
            case 4:
                SetLevel(3);
                break;
            case 5:
                SetLevel(4);
                break;
            case 6:
                SetLevel(5);
                break;
            case 7:
                SetLevel(6);
                break;
            case 8:
                SetLevel(7);
                break;
            case 9: 
                SetLevel(8);
                break;
            case 10: 
                SetLevel(9);
                break;
            case 11: 
                SetLevel(10);
                break;
            default:
                Debug.LogError("Invalid game level: " + Level_variables.level);
                break;
        }
    }

    public void SetLevel(int index)
    {
        if (index >= 0 && index < Level.Length)
        {
            for (int i = 0; i <= index; i++)
            {
                Level[i].SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Level index out of range: " + index);
        }
    }

    public void selectLevel(int level)
    {
        Debug.Log("Game level" + level);
        if(GameManager.GetCurrentGamelevel() > level)
        {
            isPlayedLevel = true;
        }
        level_menu = level;
        GameManager.setCurrentGamelevel(level);
    }

}

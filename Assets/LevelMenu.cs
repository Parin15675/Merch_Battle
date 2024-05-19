using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    public GameObject[] Level; 
    GameManager gameManager;
    public static bool check_level = false;
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

    private void SetLevel(int index)
    {

        //foreach (GameObject level in Level)
        //{
        //    level.SetActive(false);
        //}


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

    public void Level1()
    {
        Debug.Log("Game level1");
        check_level = true;
        level_menu = 1;
    }

    public void Level2()
    {
        Debug.Log("Game level2");
        check_level = true;
        level_menu = 2;
    }

    public void Level3()
    {
        Debug.Log("Game level3");
        check_level = true;
        level_menu = 3;
    }
    public void Level4()
    {
        Debug.Log("Game level4");
        check_level = true;
        level_menu = 4;
    }

    public void Level5()
    {
        Debug.Log("Game level5");
        check_level = true;
        level_menu = 5;
    }

    public void Level6()
    {
        Debug.Log("Game level6");
        check_level = true;
        level_menu = 6;
    }

    public void Level7()
    {
        Debug.Log("Game level7");
        check_level = true;
        level_menu = 7;
    }

    public void Level8()
    {
        Debug.Log("Game level8");
        check_level = true;
        level_menu = 8;
    }

    public void Level9()
    {
        Debug.Log("Game level9");
        check_level = true;
        level_menu = 9;
    }

}

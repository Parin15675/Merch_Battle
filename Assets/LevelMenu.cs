using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    public GameObject[] Level; 

    public void Update()
    {
        Debug.Log(GameManager.GetCurrentGamelevel());

        switch (GameManager.GetCurrentGamelevel()) 
        {
            case 0:
                SetLevel(0);
                break;
            case 1:
                SetLevel(1);
                break;
            case 2:
                SetLevel(2); 
                break;
            case 3:
                SetLevel(3);
                break;
            case 4:
                SetLevel(4);
                break;
            case 5:
                SetLevel(5);
                break;
            case 6:
                SetLevel(6);
                break;
            case 7:
                SetLevel(7);
                break;
            case 8: 
                SetLevel(8);
                break;
            case 9: 
                SetLevel(9);
                break;
            case 10: 
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
}

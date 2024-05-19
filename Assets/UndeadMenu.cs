using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadMenu : MonoBehaviour
{

    public GameObject[] Undeads;
    public GameObject[] Undeads_black;

    public int currentGameLevel;

    
    private void Update()
    {
        currentGameLevel = GameManager.GetCurrentGamelevel();

        if (currentGameLevel == 2)
        {
            for (int i = 0; i <= 3; i++)
            {

                Undeads_black[i].SetActive(false);
                Undeads[i].SetActive(true);

            }
        }
        else if (currentGameLevel == 3)
        {
            for (int i = 0; i <= 4; i++)
            {
                Undeads_black[i].SetActive(false);
                Undeads[i].SetActive(true);
            }
        }
        else if (currentGameLevel == 4)
        {
            for (int i = 0; i <= 5; i++)
            {
                Undeads_black[i].SetActive(false);
                Undeads[i].SetActive(true);
            }
        }
        else if (currentGameLevel == 5)
        {
            for (int i = 0; i <= 6; i++)
            {
                Undeads_black[i].SetActive(false);
                Undeads[i].SetActive(true);
            }
        }
        else if (currentGameLevel == 6)
        {
            for (int i = 0; i <= 7; i++)
            {
                Undeads_black[i].SetActive(false);
                Undeads[i].SetActive(true);
            }
        }
        else if (currentGameLevel == 7)
        {
            for (int i = 0; i <= 8; i++)
            {
                Undeads_black[i].SetActive(false);
                Undeads[i].SetActive(true);
            }
        }
        else if (currentGameLevel == 8)
        {
            for (int i = 0; i <= 9; i++)
            {
                Undeads_black[i].SetActive(false);
                Undeads[i].SetActive(true);
            }
        }
        else if (currentGameLevel > 9)
        {
            for (int i = 0; i <= 10; i++)
            {
                Undeads_black[i].SetActive(false);
                Undeads[i].SetActive(true);
            }
        }
        else if (currentGameLevel == 0)
        {
            Debug.Log("cur 0");
        }
    }
}

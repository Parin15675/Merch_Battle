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

        if (currentGameLevel == 1)
        {
            for (int i = 0; i <= 3; i++)
            {

                Undeads_black[i].SetActive(false);
                Undeads[i].SetActive(true);

            }
        }
        else if (currentGameLevel == 2)
        {
            for (int i = 0; i <= 4; i++)
            {
                Undeads_black[i].SetActive(false);
                Undeads[i].SetActive(true);
            }
        }
        else if (currentGameLevel == 3)
        {
            for (int i = 0; i <= 5; i++)
            {
                Undeads_black[i].SetActive(false);
                Undeads[i].SetActive(true);
            }
        }
    }
}

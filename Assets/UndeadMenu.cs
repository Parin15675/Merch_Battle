using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadMenu : MonoBehaviour
{

    public GameObject[] Undeads;
    public GameObject[] Undeads_black;

    
    private void Update()
    {

        if (Level_variables.level == 1)
        {
            for (int i = 0; i <= 3; i++)
            {

                Undeads_black[i].SetActive(false);
                Undeads[i].SetActive(true);

            }
        }
        else if (Level_variables.level == 2)
        {
            for (int i = 0; i <= 4; i++)
            {
                Undeads_black[i].SetActive(false);
                Undeads[i].SetActive(true);
            }
        }
        else if (Level_variables.level == 3)
        {
            for (int i = 0; i <= 5; i++)
            {
                Undeads_black[i].SetActive(false);
                Undeads[i].SetActive(true);
            }
        }
    }
}

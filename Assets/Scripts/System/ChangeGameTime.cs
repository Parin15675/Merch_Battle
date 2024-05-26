using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGameTime : MonoBehaviour
{

    [Range(0.1f, 2f)]

    public float modifiedScale;

    void Update()
    {
        Time.timeScale = modifiedScale;
    }

    public void setSpeed(float speed)
    {
        if (modifiedScale != speed) 
        {
            modifiedScale = speed;
        } else
        {
            modifiedScale = 1f;
        }       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScreen : MonoBehaviour
{
    bool isOpen;

    public void toggleScreen()
    {
        isOpen = !isOpen;   
        if (isOpen)
        {
            gameObject.transform.localPosition = new Vector3(10, 0, 1);
        } 
        else
        {
            gameObject.transform.localPosition = new Vector3(-731, 0, 1);
        }
    }
}

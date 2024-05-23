using System.Collections.Generic;
using UnityEngine;

public class speed_adjust : MonoBehaviour
{
    public bool speedx2 = false;
    public bool check = true;

    public void SpeedX2()
    { 
        if (check)
        {
            speedx2 = true;
            check = false;
        }
        else
        {
            speedx2 = false;
            check = true;
        }
        
        
    }
    
}

using UnityEngine;
using TMPro; 

public class TextUpdater : MonoBehaviour
{
    public TextMeshProUGUI textMesh; 
    private int number;
    public int dieCount = 14;
    void Start()
    {
        number = 0; 
        UpdateText(); 
    }

    
    public void UpdateText()
    {
        textMesh.text = number.ToString(); 
    }

    
    public void ChangeNumber(int newValue)
    {
        number = newValue; 
        UpdateText(); 
    }
}

using UnityEngine;
using TMPro; 

public class TextUpdater : MonoBehaviour
{
    public TextMeshProUGUI textMesh; 
    private int number;
    public int dieCount = 14;

    void Update()
    {
        UpdateText(); 
    }

    
    public void UpdateText()
    {
        textMesh.text = dieCount.ToString(); 
    }

    
    public void ChangeNumber(int newValue)
    {
        dieCount = newValue; 
        UpdateText(); 
    }
}

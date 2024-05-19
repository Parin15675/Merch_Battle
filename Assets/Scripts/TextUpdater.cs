using UnityEngine;
using TMPro; 

public class TextUpdater : MonoBehaviour
{
    public TextMeshProUGUI textMesh; 
    private int number;
    public int dieCount;
    public GameManager manager;


    private void Start()
    {
        checkLevel();
    }

    void Update()
    {
        UpdateText();
    }

    
    public void UpdateText()
    {

        textMesh.text = dieCount.ToString(); 
    }

    public void checkLevel()
    {
        if (GameManager.GetCurrentGamelevel() == 1)
        {
            dieCount = 10;
        }
        else if (GameManager.GetCurrentGamelevel() == 2)
        {
            dieCount = 10;
        }
    }

    
    public void ChangeNumber(int newValue)
    {
        dieCount = newValue; 
        UpdateText(); 
    }
}

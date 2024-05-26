using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    public GameObject characterButtonPrefab; // Prefab for the button representing the character
    public Transform contentParent; // The parent for the buttons inside the scroll view
    public GameObject[] characterPrefabs; // Array of character prefabs
    public Transform displayArea; // The area where the selected character prefab will be displayed

    void Start()
    {
        if (characterButtonPrefab == null)
        {
            Debug.LogError("Character Button Prefab is not assigned.");
            return;
        }

        if (contentParent == null)
        {
            Debug.LogError("Content Parent is not assigned.");
            return;
        }

        if (characterPrefabs == null || characterPrefabs.Length == 0)
        {
            Debug.LogError("Character Prefabs are not assigned or empty.");
            return;
        }

        if (displayArea == null)
        {
            Debug.LogError("Display Area is not assigned.");
            return;
        }

        GenerateCharacterButtons();
    }

    void GenerateCharacterButtons()
    {
        foreach (var characterPrefab in characterPrefabs)
        {
            if (characterPrefab == null)
            {
                Debug.LogError("One of the character prefabs is not assigned.");
                continue;
            }

            GameObject button = Instantiate(characterButtonPrefab, contentParent);
            Image buttonImage = button.GetComponentInChildren<Image>();

            if (buttonImage == null)
            {
                Debug.LogError("Character Button Prefab does not have an Image component.");
                continue;
            }

            SpriteRenderer characterSpriteRenderer = characterPrefab.GetComponent<SpriteRenderer>();
            if (characterSpriteRenderer == null)
            {
                Debug.LogError("Character Prefab does not have a SpriteRenderer component.");
                continue;
            }

            buttonImage.sprite = characterSpriteRenderer.sprite;

            // Add a listener to the button to handle clicks
            button.GetComponent<Button>().onClick.AddListener(() => OnCharacterButtonClicked(characterPrefab));
        }
    }

    void OnCharacterButtonClicked(GameObject characterPrefab)
    {
        // Clear previous character from the display area
        foreach (Transform child in displayArea)
        {
            Destroy(child.gameObject);
        }

        // Instantiate the selected character prefab in the display area
        GameObject characterInstance = Instantiate(characterPrefab, displayArea);
        characterInstance.transform.localPosition = Vector3.zero; // Optional: Adjust the position if necessary
    }
}

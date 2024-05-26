using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro; // Make sure to include the TextMeshPro namespace

public class ShopUIManager : MonoBehaviour, IScrollHandler
{
    public GameObject characterButtonPrefab; // Prefab for the button representing the character
    public Transform contentParent; // The parent for the buttons inside the scroll view
    public GameObject[] characterPrefabs; // Array of character prefabs
    public Transform displayArea; // The area where the selected character prefab will be displayed
    public GameObject characterUpgradePanelPrefab; // Prefab for the character upgrade panel

    private ScrollRect scrollRect; // The ScrollRect component for scrolling

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        GenerateCharacterButtons();
    }

    void GenerateCharacterButtons()
    {
        foreach (var characterPrefab in characterPrefabs)
        {
            GameObject button = Instantiate(characterButtonPrefab, contentParent);
            Image buttonImage = button.transform.Find("CharacterImage").GetComponent<Image>();
            SpriteRenderer characterSpriteRenderer = characterPrefab.GetComponent<SpriteRenderer>();
            buttonImage.sprite = characterSpriteRenderer.sprite;

            // Add a listener to the button to handle clicks
            button.GetComponent<Button>().onClick.AddListener(() => OnCharacterButtonClicked(characterPrefab));
        }
    }

    void OnCharacterButtonClicked(GameObject characterPrefab)
    {
        // Clear previous character upgrade panel from the display area
        foreach (Transform child in displayArea)
        {
            Destroy(child.gameObject);
        }

        // Instantiate the character upgrade panel in the display area
        GameObject upgradePanelInstance = Instantiate(characterUpgradePanelPrefab, displayArea);
        upgradePanelInstance.transform.localPosition = Vector3.zero; // Optional: Adjust the position if necessary

        // Get the BaseCharacter component from the character prefab
        BaseCharacter baseCharacter = characterPrefab.GetComponent<BaseCharacter>();
        if (baseCharacter == null)
        {
            Debug.LogError("Character Prefab does not have a BaseCharacter component.");
            return;
        }

        // Get the SpriteRenderer component from the character prefab
        SpriteRenderer characterSpriteRenderer = characterPrefab.GetComponent<SpriteRenderer>();
        if (characterSpriteRenderer == null)
        {
            Debug.LogError("Character Prefab does not have a SpriteRenderer component.");
            return;
        }

        // Update the upgrade panel with character stats and image
        TMP_Text nameText = upgradePanelInstance.transform.Find("Name").GetComponent<TMP_Text>();
        TMP_Text hpText = upgradePanelInstance.transform.Find("HP").GetComponent<TMP_Text>();
        TMP_Text attackText = upgradePanelInstance.transform.Find("ATK").GetComponent<TMP_Text>();
        TMP_Text speedText = upgradePanelInstance.transform.Find("SPD").GetComponent<TMP_Text>();
        Image characterImage = upgradePanelInstance.transform.Find("CharacterImage").GetComponent<Image>();

        nameText.text = characterPrefab.name;
        hpText.text = "HP: " + baseCharacter.health.ToString();
        attackText.text = "ATK: " + baseCharacter.attack.ToString();
        speedText.text = "SPD: " + baseCharacter.speed.ToString();
        characterImage.sprite = characterPrefab.GetComponent<SpriteRenderer>().sprite;
    }

    public void OnScroll(PointerEventData data)
    {
        float scrollDelta = data.scrollDelta.y;
        Vector2 newPosition = scrollRect.content.anchoredPosition + new Vector2(0, scrollDelta * -25); // Adjust the multiplier for sensitivity
        scrollRect.content.anchoredPosition = newPosition;
    }
}

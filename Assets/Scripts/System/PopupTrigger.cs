using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupTrigger : MonoBehaviour
{
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private GameObject menu;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private Vector3 popupOffset = new Vector3(1f, 1f, 0f); // Adjust the offset as needed

    private GameObject popupInstance;
    private Button activateMenuButton;
    private bool isColliding;

    private void Start()
    {
        // Ensure menu is initially inactive
        if (menu != null)
        {
            menu.SetActive(false);
        }

        StartCoroutine(CheckForNoCollisions());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && popupInstance == null)
        {
            isColliding = true;

            // Instantiate the popup prefab at the trigger's position with an offset
            Vector3 popupPosition = transform.position + popupOffset;
            popupInstance = Instantiate(popupPrefab, popupPosition, Quaternion.identity);
            popupInstance.SetActive(true);

            // Find the button in the popup prefab and add a listener to activate the menu
            activateMenuButton = popupInstance.GetComponentInChildren<Button>();
            if (activateMenuButton != null)
            {
                activateMenuButton.onClick.AddListener(ActivateMenu);
            }
            else
            {
                Debug.LogError("Button component missing from popupPrefab");
            }

            StartCoroutine(FadeIn());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = false;
        }
    }

    private IEnumerator CheckForNoCollisions()
    {
        while (true)
        {
            if (!isColliding && popupInstance != null)
            {
                StartCoroutine(FadeOut());
            }
            yield return new WaitForSeconds(0.1f); // Check every 0.1 seconds
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Vector3 originalScale = popupInstance.transform.localScale;
        popupInstance.transform.localScale = Vector3.zero;

        while (elapsedTime < fadeDuration)
        {
            if (popupInstance == null) yield break;
            float t = elapsedTime / fadeDuration;
            popupInstance.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (popupInstance != null)
        {
            popupInstance.transform.localScale = originalScale;
        }
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Vector3 originalScale = popupInstance != null ? popupInstance.transform.localScale : Vector3.one;

        while (elapsedTime < fadeDuration)
        {
            if (popupInstance == null) yield break;
            float t = elapsedTime / fadeDuration;
            popupInstance.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (popupInstance != null)
        {
            popupInstance.transform.localScale = Vector3.zero;
            Debug.Log("Destroying popup instance");
            Destroy(popupInstance);
            popupInstance = null; // Ensure reference is cleared
        }
    }

    private void ActivateMenu()
    {
        if (menu != null)
        {
            menu.SetActive(true);
        }
    }
}

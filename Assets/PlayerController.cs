using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3.0f;
    [SerializeField] private GameObject shop_popup;
    [SerializeField] private GameObject DemonBook_popup;
    [SerializeField] private GameObject Level_menu;
    [SerializeField] private float fadeDuration = 0.5f;

    private Vector2 movement;
    private Animator animator;
    private CanvasGroup shopCanvasGroup;
    private CanvasGroup DemonCanvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        shopCanvasGroup = shop_popup.GetComponent<CanvasGroup>();
        DemonCanvasGroup = DemonBook_popup.GetComponent<CanvasGroup>();
        if (shopCanvasGroup == null)
        {
            Debug.LogError("CanvasGroup component missing from shop_popup");
        }
        else
        {
            shopCanvasGroup.alpha = 0;
            shop_popup.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        animator.SetFloat("Speed", Mathf.Abs(movement.magnitude * movementSpeed));

        bool flipped = movement.x < 0;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
    }

    private void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            var xMovement = movement.x * movementSpeed * Time.deltaTime;
            var yMovement = movement.y * movementSpeed * Time.deltaTime;
            this.transform.Translate(new Vector3(xMovement, yMovement), Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Shop"))
        {
            StartCoroutine(FadeIn(shopCanvasGroup, fadeDuration, shop_popup));
            Debug.Log("Shop");
        }else if (target.CompareTag("Demon book"))
        {
            StartCoroutine(FadeIn(DemonCanvasGroup, fadeDuration, DemonBook_popup));
        }
        else if (target.CompareTag("Enter Level"))
        {
            Level_menu.SetActive(true);
            this.transform.position = new Vector3(1834.882f, 600.2556f, this.transform.position.z);
            Debug.Log("Enter Level");
        }
    }

    private void OnTriggerExit2D(Collider2D target)
    {
        if (target.CompareTag("Shop"))
        {
            StartCoroutine(FadeOut(shopCanvasGroup, fadeDuration, shop_popup));
            Debug.Log("Exit Shop");
        }else if(target.CompareTag("Demon book"))
        {
            StartCoroutine(FadeOut(DemonCanvasGroup, fadeDuration, DemonBook_popup));
        }
    }

    private IEnumerator FadeIn(CanvasGroup canvasGroup, float duration, GameObject popUp)
    {
        popUp.SetActive(true); // Ensure it's active
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup, float duration, GameObject popUp)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0;
        popUp.SetActive(false); // Ensure it's inactive
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBarTracker : MonoBehaviour
{
    float timer = 0.0f;

    [Header("Core Settings")]
    [SerializeField] private Image bar;
    [SerializeField] private int manaGenerationTime = 100;
    [SerializeField] private int currentResource = 100;
    [SerializeField] private int maxResource = 100;
    [Space]
    [SerializeField] private bool possibleOverKill;

    [Header("Animation Speed")]
    [SerializeField, Range(0, 0.5f)] private float animationTime = 0.25f;
    private Coroutine _fillRoutine;

    private void Start()
    {

    }

    private void Update()
    {
        timer += Time.deltaTime;
        float seconds = timer % 60;
        Debug.Log(seconds);
        if(seconds >= manaGenerationTime)
        {
            currentResource++;
            seconds = 0.0f;
            timer = 0.0f;
        }

        if (maxResource <= 0)
        {
            bar.fillAmount = 0;
            return;
        }

        float fillAmount = (float) currentResource / maxResource;

        bar.fillAmount = fillAmount;
    }

    public bool ChangeResourceByAmount(int amount)
    {
        if (!possibleOverKill && currentResource + amount < 0)
            return false;

        currentResource += amount;
        currentResource = Mathf.Clamp(currentResource, 0, maxResource);

        TriggerFillAnimation();

        return true;

    }

    private void TriggerFillAnimation()
    {
        float targetFill = (float)currentResource / maxResource;

        if (Mathf.Approximately(bar.fillAmount, targetFill))
            return;

        if(_fillRoutine != null)
            StopCoroutine(_fillRoutine);

        _fillRoutine = StartCoroutine(SmoothlyTransitonToNewValue(targetFill));
    }

    private IEnumerator SmoothlyTransitonToNewValue(float targetFill)
    {
        float originalFill = bar.fillAmount;
        float elapsedTime = 0.0f; 

        while (elapsedTime < animationTime) 
        {
            elapsedTime += Time.deltaTime;
            float time = elapsedTime / animationTime;
            bar.fillAmount = Mathf.Lerp(originalFill, targetFill, time);

            yield return null;
        }

        bar.fillAmount = targetFill;
    }
}


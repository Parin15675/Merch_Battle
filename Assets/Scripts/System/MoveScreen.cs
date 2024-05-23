using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScreen : MonoBehaviour
{
    public bool isOpen;

    public void toggleScreen()
    {
        isOpen = !isOpen;   
        if (isOpen)
        {
            StartCoroutine(Animate(new Vector3(10, 0, 1)));
        } 
        else
        {
            StartCoroutine(Animate(new Vector3(-731.34f, 0, 1)));
        }
    }

    private IEnumerator Animate(Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.1f;
        Vector3 from = transform.localPosition;

        while (elapsed < duration)
        {
            transform.localPosition = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = to;
    }
}

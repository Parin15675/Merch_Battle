using System.Collections;
using UnityEngine;

public class arrowAnimation : MonoBehaviour
{
    int speed = 30;
    public Animator animator;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        WalkForward();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Enemy") || target.gameObject.CompareTag("enemy wall"))
        {
            animator.SetBool("Found Enemy", true);
            speed = 0;
            StartCoroutine(DelayResetPosition());
            
        }
    }

    private void WalkForward()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }

    private IEnumerator DelayResetPosition()
    {
        yield return new WaitForSeconds(0.85f); 
        ResetPosition();
    }

    private void ResetPosition()
    {
        speed = 30;  // Reset the speed
        animator.SetBool("Found Enemy", false);
        transform.position = startPosition;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int attackDamage = 10;
    public int speed = 100;
    public Animator animator;
    public float curveHeight = 20.0f; // Adjust the height of the curve
    public float curveDuration = 1.0f; // Adjust the duration of the curve

    private List<Transform> heroes = new List<Transform>();
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float elapsedTime = 0f;

    private void Update()
    {
        if (targetPosition == Vector3.zero)
        {
            FindAllHeroes();
            Transform targetHero = GetClosestHero();

            if (targetHero != null)
            {
                targetPosition = targetHero.position;
                startPosition = transform.position;
                elapsedTime = 0f;
            }
        }
        else
        {
            MoveTowardsTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.GetType() == typeof(BoxCollider2D))
        {
            if (target.gameObject.CompareTag("Hero"))
            {
                speed = 0;
                Health heroHealth = target.gameObject.GetComponent<Health>();
                if (heroHealth != null)
                {
                    heroHealth.TakeDamage(attackDamage);
                    Debug.Log("enemy hit by arrow");
                }
                Destroy(gameObject);
            }
        }
    }

    private void FindAllHeroes()
    {
        heroes = GameObject.FindGameObjectsWithTag("Hero").Select(h => h.transform).ToList();
        if (heroes.Count == 0)
        {
            Debug.LogWarning("No heroes found.");
        }
    }

    private Transform GetClosestHero()
    {
        if (heroes == null || heroes.Count == 0) return null;

        Transform closestHero = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Transform potentialHero in heroes)
        {
            float distanceSqr = (potentialHero.position - currentPosition).sqrMagnitude;
            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestHero = potentialHero;
            }
        }

        return closestHero;
    }

    private void MoveTowardsTarget()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > curveDuration)
        {
            // Move directly towards the target if the curve duration has elapsed
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            float t = elapsedTime / curveDuration;
            Vector3 midPoint = Vector3.Lerp(startPosition, targetPosition, t);
            midPoint.y += Mathf.Sin(t * Mathf.PI) * curveHeight;
            transform.position = Vector3.Lerp(transform.position, midPoint, speed * Time.deltaTime);
        }

        // Calculate the angle in radians
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        // Apply rotation to the arrow
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Destroy the projectile if it reaches the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}

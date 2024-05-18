using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroProjectile : MonoBehaviour
{
    public int attackDamage = 10;
    public int speed = 100;
    public Animator animator;
    public float curveHeight = 20.0f; // Adjust the height of the curve
    public float curveDuration = 1.0f; // Adjust the duration of the curve

    private List<Transform> enemies = new List<Transform>();
    private Transform targetEnemy;
    private Vector3 previousTarget = new Vector3(0f,0f,0f);
    private Vector3 startPosition;
    private float elapsedTime = 0f;

    private void Update()
    {
        if (targetEnemy == null)
        {
            FindAllEnemies();
            targetEnemy = GetClosestEnemy();

            if (targetEnemy != null)
            {

                if (previousTarget != new Vector3(0f, 0f, 0f) && targetEnemy.transform.position != previousTarget)
                {
                    Destroy(gameObject);
                    return;
                }

                previousTarget = targetEnemy.position;
                startPosition = transform.position;
                elapsedTime = 0f;
            }
        }
        else
        {
            MoveTowardsEnemy();
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.GetType() == typeof(BoxCollider2D))
        {
            if (target.gameObject.CompareTag("Enemy"))
            {
                speed = 0;
                HealthEnemy enemyHealth = target.gameObject.GetComponent<HealthEnemy>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attackDamage);
                    Debug.Log("enemy hit by arrow");
                }
                Destroy(gameObject);
            }
        }
    }

    private void FindAllEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Select(h => h.transform).ToList();
        if (enemies.Count == 0)
        {
            Debug.LogWarning("No enemies found.");
        }
    }

    private Transform GetClosestEnemy()
    {
        if (enemies == null || enemies.Count == 0) return null;

        Transform closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Transform potentialEnemy in enemies)
        {
            float distanceSqr = (potentialEnemy.position - currentPosition).sqrMagnitude;
            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestEnemy = potentialEnemy;
            }
        }

        return closestEnemy;
    }

    private void MoveTowardsEnemy()
    {
        if (targetEnemy == null)
        {
            Destroy(gameObject); // Destroy the projectile if the target is null
            return;
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime > curveDuration)
        {
            // Move directly towards the target if the curve duration has elapsed
            Vector3 direction = (targetEnemy.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            float t = elapsedTime / curveDuration;
            Vector3 midPoint = Vector3.Lerp(startPosition, targetEnemy.position, t);
            midPoint.y += Mathf.Sin(t * Mathf.PI) * curveHeight;
            transform.position = Vector3.Lerp(transform.position, midPoint, speed * Time.deltaTime);
        }

        // Calculate the angle in radians
        Vector3 directionToTarget = (targetEnemy.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        // Apply rotation to the arrow
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}

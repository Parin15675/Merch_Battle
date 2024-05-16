using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int attackDamage = 10;
    public int speed = 100;
    public Animator animator;

    private List<Transform> enemies = new List<Transform>();
    private Transform targetEnemy;


    private void Update()
    {
        if (targetEnemy == null)
        {
            FindAllEnemies();
            targetEnemy = GetClosestEnemy();
        }
        else if (targetEnemy != null)
        {
            MoveTowardsEnemy();
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Enemy") || target.gameObject.CompareTag("enemy wall"))
        {
            animator.SetBool("Found Enemy", true);
            speed = 0;
            HealthEnemy enemyHealth = target.gameObject.GetComponent<HealthEnemy>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
                Debug.Log("enemy hit by arrow");
                animator.SetBool("Found Enemy", false);
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
        Vector3 direction = (targetEnemy.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public float speed = 30.0f;
    public static HeroMovement activeHero;  // Static reference to the currently active hero

    private List<Transform> enemies = new List<Transform>();
    private Transform targetEnemy;
    private bool canMove = true;
    public Animator animator;
    public int point = 1;

    void Start()
    {
        FindAllEnemies();
    }

    void Update()
    {
        if (canMove)
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
            else
            {
                WalkForward();
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

    public void StopMovement()
    {
        canMove = false;
        speed = 0;
    }

    private void WalkForward()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void StartMovement(float newSpeed)
    {
        speed = newSpeed;  // Set new speed
        canMove = true;
        Debug.Log("Movement restarted at speed: " + newSpeed);
    }
}

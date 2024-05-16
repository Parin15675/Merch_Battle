using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    private BaseCharacter baseCharacter;
    private List<Transform> enemies = new List<Transform>();
    private Transform targetEnemy;
    private bool canMove = true;

    public float speed = 30.0f;
    public Animator animator;
    public int point = 1;
    public float avoidanceDistance = 1.0f; // Distance to move away to avoid overlap

    private CapsuleCollider2D capsuleCollider;
    private Vector3 avoidanceDirection;

    private void Awake()
    {
        baseCharacter = GetComponent<BaseCharacter>();
        speed = baseCharacter.speed;
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

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

            // Apply avoidance direction if necessary
            if (avoidanceDirection != Vector3.zero)
            {
                transform.position += avoidanceDirection * speed * Time.deltaTime;
                avoidanceDirection = Vector3.zero; // Reset avoidance direction after applying it
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

    public void WalkForward()
    {
        speed = baseCharacter.speed;
        canMove = true;
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            // Calculate the direction to move away from the overlapping object
            Vector3 direction = (transform.position - other.transform.position).normalized;
            avoidanceDirection = direction * avoidanceDistance;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            // Reset the avoidance direction when no longer overlapping
            avoidanceDirection = Vector3.zero;
        }
    }
}

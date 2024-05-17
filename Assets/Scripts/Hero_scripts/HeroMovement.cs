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
    public float avoidanceForce = 5.0f; // Force to move away to avoid overlap
    public float avoidanceDamping = 0.9f; // Damping factor to smooth out the avoidance movement

    private CapsuleCollider2D capsuleCollider;
    private Vector3 avoidanceDirection;

    private void Awake()
    {
        baseCharacter = GetComponent<BaseCharacter>();
        speed = baseCharacter.speed;
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        if (capsuleCollider == null)
        {
            capsuleCollider = gameObject.AddComponent<CapsuleCollider2D>();
            capsuleCollider.isTrigger = true; // Make the capsule collider a trigger
        }
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
                transform.position += avoidanceDirection * Time.deltaTime;
                avoidanceDirection *= avoidanceDamping; // Apply damping to the avoidance direction

                // If the avoidance direction is almost negligible, reset it
                if (avoidanceDirection.magnitude < 0.01f)
                {
                    avoidanceDirection = Vector3.zero;
                }
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
            avoidanceDirection = direction * avoidanceForce;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            // Continue to push away while still overlapping
            Vector3 direction = (transform.position - other.transform.position).normalized;
            avoidanceDirection = direction * avoidanceForce;
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

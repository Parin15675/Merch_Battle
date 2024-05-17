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
    public float checkDistance = 2.0f; // Distance to check for heroes in front
    public LayerMask heroLayer; // Layer to detect other heroes

    private CapsuleCollider2D capsuleCollider;
    private Vector3 avoidanceDirection;
    private Vector3 directionToMove;

    speed_adjust Speed_adjust;

    private void Awake()
    {
        baseCharacter = GetComponent<BaseCharacter>();
        speed = baseCharacter.speed;
        Speed_adjust = GameObject.FindGameObjectWithTag("speed").GetComponent<speed_adjust>();
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
        if (Speed_adjust.speedx2)
        {
            Vector3 direction = (targetEnemy.position - transform.position).normalized;
            directionToMove = direction * (speed*2) * Time.deltaTime;
            transform.position += directionToMove;
        }
        else
        {
            Vector3 direction = (targetEnemy.position - transform.position).normalized;
            directionToMove = direction * speed * Time.deltaTime;
            transform.position += directionToMove;
        }



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

}

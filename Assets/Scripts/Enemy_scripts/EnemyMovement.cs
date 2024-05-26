using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private BaseCharacter baseCharacter;
    private List<Transform> heroes = new List<Transform>();
    private Transform targetHero;
    private bool canMove = true;

    public float speed = 30.0f;
    public Animator animator;
    public int point = 1;
    public float avoidanceForce = 5.0f; // Force to move away to avoid overlap
    public float avoidanceDamping = 0.9f; // Damping factor to smooth out the avoidance movement

    private CapsuleCollider2D capsuleCollider;
    private Vector3 avoidanceDirection;
    private Vector3 directionToMove;

    private void Awake()
    {
        baseCharacter = GetComponent<BaseCharacter>();
        speed = -baseCharacter.speed;
    }

    void Start()
    {
        FindAllHeroes();
    }

    void Update()
    {
        if (canMove)
        {
            if (targetHero == null)
            {
                FindAllHeroes();
                targetHero = GetClosestHero();
            }
            else if (targetHero != null)
            {
                MoveTowardsHero();
            }
            else
            {
                WalkForward();
            }
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

    private void MoveTowardsHero()
    {
        Vector3 direction = (targetHero.position - transform.position).normalized;
        directionToMove = direction * speed * Time.deltaTime;
        transform.position -= directionToMove;
        FlipSprite(direction.x);
    }

    public void StopMovement()
    {
        canMove = false;
        speed = 0;
    }

    public void WalkForward()
    {
        speed = -baseCharacter.speed;
        canMove = true;
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        FlipSprite(1); // Always facing right when walking forward
    }

    private void FlipSprite(float directionX)
    {
        if (directionX < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (directionX > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision.GetType() == typeof(BoxCollider2D) && GetComponent<RangeEnemyAttack>() == null)
        {
            EnemyHit enemyHit = collision.GetComponent<EnemyHit>();
            if (enemyHit != null && enemyHit.isAttacking)
            {
                MoveUpOrDown();
            }
        }
    }

    private void MoveUpOrDown()
    {
        Vector3 moveDirection = Vector3.up; // Default to move up

        // Check if moving up is clear
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 1.0f, LayerMask.GetMask("Enemy"));
        if (hitUp.collider != null)
        {
            // If there's an enemy above, move down instead
            moveDirection = Vector3.down;
        }

        transform.Translate(moveDirection * speed * Time.deltaTime);
        Debug.Log("Moving " + moveDirection);
    }
}

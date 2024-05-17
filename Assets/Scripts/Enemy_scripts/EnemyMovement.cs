using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private BaseCharacter baseCharacter;
    private bool canMove = true;
    private Transform currentTarget;
    private List<Transform> targets;
    private Vector3 directionToMove;

    public float speed = 30.0f;

    speed_adjust Speed_adjust;

    private void Awake()
    {
        baseCharacter = GetComponent<BaseCharacter>();
        speed = -baseCharacter.speed;
        Speed_adjust = GameObject.FindGameObjectWithTag("speed").GetComponent<speed_adjust>();
    }

    void Update()
    {
        if (canMove)
        {
            FindAllTargets();
            currentTarget = GetClosestTarget();

            if (currentTarget != null)
            {
                MoveTowardsTarget();
            }
            else
            {
                WalkForward();
            }
        }
    }

    private Transform GetClosestTarget()
    {
        if (targets == null || targets.Count == 0) return null;

        Transform closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Transform potentialTarget in targets)
        {
            float distanceSqr = (potentialTarget.position - currentPosition).sqrMagnitude;
            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestTarget = potentialTarget;
            }
        }

        return closestTarget;
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        directionToMove = direction * (Speed_adjust.speedx2 ? speed * 2 : speed) * Time.deltaTime;
        transform.position -= directionToMove;
        FlipSprite();
    }

    private void FindAllTargets()
    {
        targets = GameObject.FindGameObjectsWithTag("Hero")
                            .Select(hero => hero.transform)
                            .ToList();
    }

    public void WalkForward()
    {
        canMove = true;
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    public void StopMovement()
    {
        canMove = false;
        speed = 0;
    }

    public void StartMovement()
    {
        canMove = true;
        speed = -baseCharacter.speed; // Reset to default speed or set to a desired value
    }

    private void FlipSprite()
    {
        if (speed < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (speed > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}

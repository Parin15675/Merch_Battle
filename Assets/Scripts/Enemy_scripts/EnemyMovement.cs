using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = -30.0f;
    private bool canMove = true;
    private Transform currentTarget;
    private List<Transform> targets;

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
        transform.position -= direction * speed * Time.deltaTime;
    }

    private void FindAllTargets()
    {
        targets = GameObject.FindGameObjectsWithTag("Hero")
                            .Select(hero => hero.transform)
                            .ToList();
    }

    private void WalkForward()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void StopMovement()
    {
        canMove = false;
        speed = 0;
    }

    public void StartMovement()
    {
        canMove = true;
        speed = -30f; // Reset to default speed or set to a desired value
    }
}

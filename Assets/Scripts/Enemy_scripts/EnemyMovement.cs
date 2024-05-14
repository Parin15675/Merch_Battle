using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float speed = -30.0f; 
    private bool canMove = true;
    private Transform targetEnemy;
    private List<Transform> enemies;

    void Update()
    {
        FindAllEnemies();
        targetEnemy = GetClosestEnemy(enemies);

        if (canMove)
        {   if (targetEnemy == null)
            {
                WalkForward();
            } 
            else
            {
                MoveTowardsEnemy();
            }
            
        }
        else
        {
            transform.Translate(new Vector3(0, 0, 0));
        }
    }

    Transform GetClosestEnemy(List<Transform> enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

    private void MoveTowardsEnemy()
    {
        Vector3 direction = (targetEnemy.position - transform.position).normalized;
        transform.position -= direction * speed * Time.deltaTime;
    }

    private void FindAllEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Hero").Select(h => h.transform).ToList();
        if (enemies.Count == 0)
        {
            Debug.LogWarning("No enemies found.");
        }
    }

    private void WalkForward()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }

    public void StopMovement()
    {
        canMove = false;
        speed = 0;
    }

    public void StartMovement(float newSpeed)
    {
        speed = newSpeed; // Set new speed if needed
        canMove = true;
        Debug.Log("Movement restarted at speed: " + newSpeed);
    }

}



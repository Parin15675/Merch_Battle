using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public static HeroMovement activeHero;  // Static reference to the currently active hero

    private List<Transform> enemies = new List<Transform>();
    private Transform targetEnemy;
    private bool canMove = true;

    void Start()
    {
        FindAllEnemies();
    }

    void Update()
    {
        // Check if this instance is the active hero
        if (this == activeHero)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectTargetEnemyWithMouse();
            }

            if (canMove && targetEnemy != null)
            {
                MoveTowardsEnemy();
            }
            else
            {
                WalkForward();
            }
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

    void OnMouseDown()  // This function is called when this GameObject is clicked
    {
        if (activeHero != this)
        {
            Debug.Log($"Control switched to hero: {gameObject.name}");
            activeHero = this;  // Set this instance as the active hero
        }
    }

    // Find all enemies in the scene
    private void FindAllEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Select(h => h.transform).ToList();
        if (enemies.Count == 0)
        {
            Debug.LogWarning("No enemies found.");
        }
    }

    private void SelectTargetEnemyWithMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            targetEnemy = hit.transform;
            Debug.Log($"Player selected enemy: {targetEnemy.name}");
        }
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
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }

    public void StartMovement(float newSpeed)
    {
        speed = newSpeed;  // Set new speed
        canMove = true;
        Debug.Log("Movement restarted at speed: " + newSpeed);
    }
}

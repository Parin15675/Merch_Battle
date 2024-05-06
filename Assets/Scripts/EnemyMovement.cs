using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float detectionRadius = 10.0f;
    private List<Transform> heroes = new List<Transform>();
    private Transform targetHero;

    private bool canMove = true;

    private void Start()
    {
        FindAllHeroes();
        SelectTargetHero();
    }

    private void Update()
    {
        heroes.RemoveAll(hero => hero == null);
        if (canMove && targetHero != null)
        {
            MoveTowardsHero();
        }
        else
        {
            FindAllHeroes();
            SelectTargetHero();
            WalkForward();
        }
    }

    // Find all heroes in the scene
    private void FindAllHeroes()
    {
        heroes = GameObject.FindGameObjectsWithTag("Hero").Select(h => h.transform).ToList();
        if (heroes.Count == 0)
        {
            Debug.LogWarning("No heroes found.");
        }
    }

    // Select the best target hero based on the attacking state
    private void SelectTargetHero()
    {
        if (heroes.Any())
        {
            // Prefer heroes that are not attacking
            var nonAttackingHeroes = heroes.Where(h => !h.GetComponent<HeroHit>().isAttacking).ToList();
            if (nonAttackingHeroes.Any())
            {
                targetHero = nonAttackingHeroes.OrderBy(h => Vector3.Distance(transform.position, h.position)).First();
            }
            else
            {
                // If all are attacking, go to the closest one
                targetHero = heroes.OrderBy(h => Vector3.Distance(transform.position, h.position)).First();
            }

            Debug.Log($"Targeting hero: {targetHero.name}");
        }
    }

    // Move towards the current target hero
    private void MoveTowardsHero()
    {
        float distance = Vector3.Distance(transform.position, targetHero.position);
        //if (distance < detectionRadius)
        //{
        Vector3 direction = (targetHero.position - transform.position).normalized;
        transform.position -= direction * speed * Time.deltaTime;
        //}
    }

    public void StopMovement()
    {
        canMove = false;
        speed = 0;
    }

    public void StartMovement()
    {
        canMove = true;
        speed = -30;
        Debug.Log("StartMovement");
    }
    private void WalkForward()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }

}


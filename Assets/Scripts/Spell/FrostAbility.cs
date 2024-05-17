using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrostAbility : Ability
{
    public GameObject icePrefab; // Ice prefab to be spawned
    private List<EnemyMovement> affectedEnemies = new List<EnemyMovement>();
    private Dictionary<EnemyMovement, GameObject> enemyIceInstances = new Dictionary<EnemyMovement, GameObject>();

    private void modifyStartPosition(Vector3 parentPos)
    {
        gameObject.transform.position = parentPos;
    }

    private void Update()
    {

    }

    public override void Activate(GameObject parent)
    {
        transform.SetParent(parent.transform);
        transform.SetAsLastSibling();
        modifyStartPosition(parent.transform.position);
    }

    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Enemy"))
        {
            EnemyMovement enemyMovement = target.gameObject.GetComponent<EnemyMovement>();
            if (enemyMovement != null && !affectedEnemies.Contains(enemyMovement))
            {
                affectedEnemies.Add(enemyMovement);
                StartCoroutine(Frosting(enemyMovement));
            }
        }
    }

    public void OnTriggerExit2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Enemy"))
        {
            EnemyMovement enemyMovement = target.gameObject.GetComponent<EnemyMovement>();
            if (enemyMovement != null && affectedEnemies.Contains(enemyMovement))
            {
                affectedEnemies.Remove(enemyMovement);
                enemyMovement.enabled = true;

                if (enemyIceInstances.ContainsKey(enemyMovement))
                {
                    Destroy(enemyIceInstances[enemyMovement]);
                    enemyIceInstances.Remove(enemyMovement);
                }
            }
        }
    }

    private IEnumerator Frosting(EnemyMovement enemyMovement)
    {
        // Disable enemy movement
        enemyMovement.enabled = false;

        // Spawn ice prefab at the enemy's position
        GameObject iceInstance = Instantiate(icePrefab, enemyMovement.transform.position, Quaternion.identity);
        iceInstance.transform.SetParent(enemyMovement.transform);
        enemyIceInstances[enemyMovement] = iceInstance;

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Enable enemy movement
        enemyMovement.enabled = true;

        // Destroy the ice instance
        if (enemyIceInstances.ContainsKey(enemyMovement))
        {
            Destroy(enemyIceInstances[enemyMovement]);
            enemyIceInstances.Remove(enemyMovement);
        }

        // Remove from affected enemies list after the effect is over
        affectedEnemies.Remove(enemyMovement);
    }
}

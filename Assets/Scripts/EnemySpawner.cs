using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private RectTransform spawnArea; 
    [SerializeField] private Transform targetObject;
    
    private int offset;
    private int padding;
    private bool isSpawnFinish = true;

    public int gameLevel;
    public float waitSpawn;

    private void Start()
    {
        gameLevel = gameManager.level;
    }

    private void Update()
    {
        
        if (isSpawnFinish)
        {
            StartCoroutine(SpawnHeroRoutine());
        }
    }

    private void adjustOffset(int amountOfEnemy)
    {
        switch (amountOfEnemy)
        {
            case 0:
                offset += 0; break;
            case 1: 
                offset -= 100; break;
            case 2:
                offset -= 50; break;
            case 3:
                break;
            case 4:
                padding -= 35; break;
            default:
                offset += 0; break;
        }
    }

    private IEnumerator SpawnHeroRoutine()
    {
        JSONReader reader = this.GetComponent<JSONReader>();
        foreach (var spawner in reader.enemySpawnerData.enemyspawner)
        {
            Debug.Log(spawner.level);
            if (spawner.level == gameLevel)
            {
                foreach (var sequence in spawner.spawnsequences)
                {
                    Debug.Log(sequence.name);

                    offset = 100;
                    padding = 100;
                    adjustOffset(sequence.amount);

                    for (int numberOfEnemy = 0; numberOfEnemy < sequence.amount; numberOfEnemy++)
                    {
                        GameObject enemyOnField = Instantiate(Resources.Load<GameObject>("Prefabs/Undead/" + sequence.name), spawnArea, false);
                        //GameObject enemyOnField = Instantiate(enemyToSpawn.enemy, spawnArea, false);
                        Vector3 position = spawnArea.InverseTransformPoint(targetObject.position);
                        enemyOnField.transform.SetParent(GameObject.Find("EnemySpawner").transform);
                        enemyOnField.GetComponent<RectTransform>().anchoredPosition = new Vector2(position.x, position.y - offset + padding * numberOfEnemy);

                        Debug.Log("Hero spawned at: " + enemyOnField.GetComponent<RectTransform>().anchoredPosition);
                    }

                    isSpawnFinish = false;
                    yield return new WaitForSeconds(waitSpawn);
                }
                
            }
        }

    }
}

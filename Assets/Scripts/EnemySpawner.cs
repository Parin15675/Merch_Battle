using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [Serializable]
    public struct spawnSequences
    {
        public GameObject enemy;
        public int amount;
    }

    [SerializeField] private RectTransform spawnArea; 
    [SerializeField] private Transform targetObject;
    [SerializeField] private List<spawnSequences> spawnSequence;

    private int offset;
    private int padding;

    public int count;
    public float waitSpawn;

    private void Start()
    {
        StartCoroutine(SpawnHeroRoutine());
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

        foreach (spawnSequences enemyToSpawn in spawnSequence)
        {
            offset = 100;
            padding = 100;
            adjustOffset(enemyToSpawn.amount);
            for (int numberOfEnemy = 0; numberOfEnemy < enemyToSpawn.amount; numberOfEnemy++)
            {
                GameObject enemyOnField = Instantiate(enemyToSpawn.enemy, spawnArea, false);
                Vector3 position = spawnArea.InverseTransformPoint(targetObject.position);
                enemyOnField.transform.SetParent(GameObject.Find("EnemySpawner").transform);
                enemyOnField.GetComponent<RectTransform>().anchoredPosition = new Vector2(position.x, position.y - offset + padding * numberOfEnemy);

                Debug.Log("Hero spawned at: " + enemyOnField.GetComponent<RectTransform>().anchoredPosition);
            }
            yield return new WaitForSeconds(waitSpawn);
        }
    }

}

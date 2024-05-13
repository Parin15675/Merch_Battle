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

    public int count;
    public float waitSpawn;

    private void Start()
    {
        StartCoroutine(SpawnHeroRoutine());
    }

    private IEnumerator SpawnHeroRoutine()
    {
        foreach (spawnSequences enemyToSpawn in spawnSequence)
        {
            for (int numberOfEnemy = 0; numberOfEnemy < enemyToSpawn.amount; numberOfEnemy++)
            {
                GameObject enemyOnField = Instantiate(enemyToSpawn.enemy, spawnArea, false);
                Vector3 position = spawnArea.InverseTransformPoint(targetObject.position);
                enemyOnField.transform.SetParent(GameObject.Find("Panel").transform);
                enemyOnField.GetComponent<RectTransform>().anchoredPosition = new Vector2(position.x, position.y + 100 * numberOfEnemy);

                Debug.Log("Hero spawned at: " + enemyOnField.GetComponent<RectTransform>().anchoredPosition);  
            }
            yield return new WaitForSeconds(waitSpawn);
        }
    }

}

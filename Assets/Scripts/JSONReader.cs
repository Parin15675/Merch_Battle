using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    public TextAsset textJSON;

    [System.Serializable]
    public class SpawnSequenceData
    {
        public string name;
        public int amount;
    }

    [System.Serializable]
    public class EnemySpawner
    {
        public int level;
        public List<SpawnSequenceData> spawnsequences;
    }

    [System.Serializable]
    public class EnemySpawnerData
    {
        public List<EnemySpawner> enemyspawner;
    }

    public EnemySpawnerData enemySpawnerData = new EnemySpawnerData();

    void Start()
    {
        LoadEnemySpawnerData();
    }

    void LoadEnemySpawnerData()
    {
        if (textJSON != null)
        {
            string dataAsJson = textJSON.text;
            enemySpawnerData = JsonUtility.FromJson<EnemySpawnerData>(dataAsJson);
            Debug.Log("Enemy Spawner Data Loaded");
        }
        else
        {
            Debug.LogError("TextAsset is null!");
        }
    }
}

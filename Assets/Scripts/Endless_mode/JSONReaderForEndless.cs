using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONReaderForEndless : MonoBehaviour
{
    public TextAsset textJSON;

    [System.Serializable]
    public class EnemySpawnerData
    {
        public List<string> enemies;
    }

    public EnemySpawnerData enemySpawnerData = new EnemySpawnerData();

    private void Awake()
    {
        LoadEnemySpawnerData();
    }

    void LoadEnemySpawnerData()
    {
        if (textJSON != null)
        {
            string dataAsJson = textJSON.text;
            enemySpawnerData = JsonUtility.FromJson<EnemySpawnerData>(dataAsJson);

            // Debugging information
            Debug.Log("JSON Data: " + dataAsJson);
            Debug.Log("Enemy Spawner Data Loaded: " + enemySpawnerData.enemies.Count + " enemies defined.");
        }
        else
        {
            Debug.LogError("TextAsset is null!");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Jason_Endless : MonoBehaviour
{
    public TextAsset textJSON;

    [System.Serializable]
    public class EnemySpawnerData
    {
        public Dictionary<string, List<string>> enemyspawner;
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
            enemySpawnerData = JsonUtility.FromJson<EnemySpawnerDataWrapper>(dataAsJson).ToEnemySpawnerData();
            Debug.Log("Enemy Spawner Data Loaded");
            foreach (var level in enemySpawnerData.enemyspawner)
            {
                Debug.Log("Level " + level.Key + ": " + string.Join(", ", level.Value));
            }
        }
        else
        {
            Debug.LogError("TextAsset is null!");
        }
    }

    [System.Serializable]
    private class EnemySpawnerDataWrapper
    {
        public List<LevelEnemies> levels;

        public EnemySpawnerData ToEnemySpawnerData()
        {
            var data = new EnemySpawnerData
            {
                enemyspawner = new Dictionary<string, List<string>>()
            };

            foreach (var level in levels)
            {
                data.enemyspawner[level.level.ToString()] = level.enemies;
            }

            return data;
        }
    }

    [System.Serializable]
    private class LevelEnemies
    {
        public int level;
        public List<string> enemies;
    }
}

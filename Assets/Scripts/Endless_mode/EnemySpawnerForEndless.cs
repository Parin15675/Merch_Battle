using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawnerForEndless : MonoBehaviour
{
    [SerializeField] private RectTransform spawnArea;
    [SerializeField] private Transform targetObject;
    [SerializeField] private TextMeshProUGUI waveNumber;

    public Continued_script continued_Script;

    private float spawnTimer;
    private int wave;
    //public int currentWave = 1;

    private JSONReaderForEndless reader;

    public int waveDuration = 30; // Duration of each wave in seconds
    private float waveTimer;
    private float spawnInterval;

    private List<GameObject> enemiesToSpawn = new List<GameObject>();
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private int enemiesPerGroup = 1; // Number of enemies to spawn per group

    public bool check_con = false;

    public GameObject Con_screen;
    public GameManager GameManager;
    //[SerializeField] private GameObject enemyWallPrefab;
    //[SerializeField] private Transform wallSpawnLocation;
    


    private void Start()
    {
        continued_Script.currentWave++;
        reader = GetComponent<JSONReaderForEndless>();
        GenerateWave();
    }

    private void FixedUpdate()
    {
        // Handle spawning of enemies
        if (spawnTimer <= 0)
        {
            if (enemiesToSpawn.Count > 0)
            {
                Vector3 startPosition = spawnArea.InverseTransformPoint(targetObject.position);

                for (int i = 0; i < enemiesPerGroup && enemiesToSpawn.Count > 0; i++)
                {
                    // Calculate the grid position
                    int row = i % 5;
                    int column = i % 5;
                    float xOffset = column * 50 + Random.Range(-25, 25); // Adjust spacing between columns
                    float yOffset = row * 50 + Random.Range(-25, 25); // Adjust spacing between rows

                    // Spawn an enemy
                    GameObject enemy = Instantiate(enemiesToSpawn[0], spawnArea, false);
                    enemiesToSpawn.RemoveAt(0); // Remove it from the list
                    spawnedEnemies.Add(enemy);

                    // Set position within the grid
                    Vector3 position = new Vector2(startPosition.x + xOffset, startPosition.y - yOffset);
                    enemy.transform.SetParent(GameObject.Find("EnemySpawner").transform);
                    enemy.GetComponent<RectTransform>().anchoredPosition = position;

                    Debug.Log("Enemy spawned at: " + enemy.GetComponent<RectTransform>().anchoredPosition);
                }

                spawnTimer = spawnInterval;
            }
            else
            {
                waveTimer = 0; // End wave if no enemies remain
                
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
        }

        // Handle wave timer
        if (waveTimer <= 0 && spawnedEnemies.Count == 0 && GameManager.enemyCastle == null)
        {
            Con_screen.SetActive(true);
            //if (check_con)
            //{
            //    Con_screen.SetActive(false);
            //    currentWave++;
            //    GenerateWave();
            //    check_con = false;
            //    //SpawnEnemyWall(wallSpawnLocation);
            //}
        }
        else
        {
            waveTimer -= Time.fixedDeltaTime;
        }

        // Clean up defeated enemies
        spawnedEnemies.RemoveAll(enemy => enemy == null);
    }


    private void GenerateWave()
    {
        waveNumber.text = "Wave " + continued_Script.currentWave;
        int waveValue = continued_Script.currentWave * 10;
        GenerateEnemies(waveValue);

        spawnInterval = waveDuration / (float)enemiesToSpawn.Count; // Fixed time between each enemy group
        waveTimer = waveDuration; // Reset wave timer
        spawnTimer = 0; // Start spawning immediately

        // Increase the number of enemies per group every few waves
        enemiesPerGroup = Mathf.Min(5, continued_Script.currentWave / 5 + 3);

        // Spawn an enemy wall at the specified location at the start of each wave
        
    }


    private void GenerateEnemies(int waveValue)
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0 && generatedEnemies.Count < 50)
        {
            int randEnemyId = Random.Range(0, reader.enemySpawnerData.enemies.Count);
            string enemyName = reader.enemySpawnerData.enemies[randEnemyId];
            GameObject enemyPrefab = Resources.Load<GameObject>("Prefabs/Undead/" + enemyName);
            int randEnemyCost = 2; // You may replace this with an actual cost based on your logic

            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    //private void SpawnEnemyWall(Transform spawnLocation)
    //{
    //    // Instantiate the enemy wall at the specified location
    //    GameObject enemyWall = Instantiate(enemyWallPrefab, spawnLocation.position, Quaternion.identity);

    //    // Set the enemy wall as a child of the specified transform
    //    enemyWall.transform.SetParent(spawnLocation, false);

    //    Debug.Log("Enemy wall spawned at: " + enemyWall.transform.position);
    //}

}

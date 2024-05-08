using UnityEngine;
using System.Collections;
using System.Threading;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] heroPrefabs; // Array of hero prefabs with RectTransform
    [SerializeField] private RectTransform spawnArea; // Area within the Canvas
    [SerializeField] private Transform targetObject; // Target object to base the spawn position on
    public int count;
    public float waitSpawn;

    private void Start()
    {
        SpawnHero();
        StartCoroutine(SpawnHeroRoutine());
    }

    private IEnumerator SpawnHeroRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitSpawn); // Spawns every 5 seconds
            SpawnHero();
        }
    }

    private void SpawnHero()
    {

        if (count > 0)
        {
            count--;
            
            // Check if the necessary components are set and the heroPrefabs array is not empty
            if (heroPrefabs.Length == 0 || spawnArea == null || targetObject == null)
            {
                Debug.LogError("Hero prefabs, Spawn Area, or Target Object not set or array is empty!");
                
            }

            // Select a random prefab from the array safely
            GameObject selectedPrefab = heroPrefabs[Random.Range(0, heroPrefabs.Length)];
            if (selectedPrefab == null)
            {
                Debug.LogError("Selected prefab is null!");
                return; 
            }

            // Instantiate the selected prefab as a child of spawnArea and position it based on the targetObject's position
            GameObject heroInstance = Instantiate(selectedPrefab, spawnArea, false);
            Vector3 position = spawnArea.InverseTransformPoint(targetObject.position);
            heroInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(position.x, position.y);

            Debug.Log("Hero spawned at: " + heroInstance.GetComponent<RectTransform>().anchoredPosition);
        }

        else
        {
            Debug.Log("count = 0");
        }
    }

}

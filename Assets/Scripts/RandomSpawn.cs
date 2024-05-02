using UnityEngine;
using System.Collections;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] heroPrefabs; // Array of hero prefabs with RectTransform
    [SerializeField] private RectTransform spawnArea; // Area within the Canvas
    [SerializeField] private Transform targetObject; // Target object to base the spawn position on

    private void Start()
    {
        StartCoroutine(SpawnHeroRoutine());
    }

    private IEnumerator SpawnHeroRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); // Spawns every 5 seconds
            SpawnHero();
        }
    }

    private void SpawnHero()
    {
        if (heroPrefabs.Length == 0 || spawnArea == null || targetObject == null)
        {
            Debug.LogError("Hero prefabs, Spawn Area, or Target Object not set!");
            return;
        }

        // Select a random prefab from the array
        GameObject selectedPrefab = heroPrefabs[Random.Range(0, heroPrefabs.Length)];
        GameObject heroInstance = Instantiate(selectedPrefab, spawnArea, false);

        // Set the position to be the same as the target object
        Vector3 position = spawnArea.InverseTransformPoint(targetObject.position);
        heroInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(position.x, position.y);

        Debug.Log("Hero spawned at: " + heroInstance.GetComponent<RectTransform>().anchoredPosition);
    }
}

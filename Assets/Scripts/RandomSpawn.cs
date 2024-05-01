using UnityEngine;
using System.Collections;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField] private GameObject heroUIPrefab; // UI Prefab with RectTransform
    [SerializeField] private RectTransform spawnArea; // Area within the Canvas

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
        if (heroUIPrefab == null || spawnArea == null)
        {
            Debug.LogError("Prefab or Spawn Area not set!");
            return;
        }

        GameObject heroInstance = Instantiate(heroUIPrefab, spawnArea, false);
        heroInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(85, 0); // Fixed position at (85, 0)

        Debug.Log("Hero spawned at: " + heroInstance.GetComponent<RectTransform>().anchoredPosition);
    }
}

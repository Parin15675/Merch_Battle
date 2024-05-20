using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgrade : MonoBehaviour
{
    public DragToSpawn tile;
    public WaveEndScreen waveEndScreen;

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void UpgradeAttack()
    {
        foreach (var hero in tile.HeroPrefab)
        {
            hero.GetComponent<BaseCharacter>().attack += 10;
        }
        Destroy(gameObject);
        waveEndScreen.Setup();
    }

    public void UpgradeHealth()
    {
        foreach (var hero in tile.HeroPrefab)
        {
            hero.GetComponent<BaseCharacter>().health += 10;
        }
        Destroy(gameObject);
        waveEndScreen.Setup();
    }

    public void UpgradeSpeed()
    {
        foreach (var hero in tile.HeroPrefab)
        {
            hero.GetComponent<BaseCharacter>().speed += 10;
        }
        Destroy(gameObject);
        waveEndScreen.Setup();
    }
}

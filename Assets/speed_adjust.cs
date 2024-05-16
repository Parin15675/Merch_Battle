using System.Collections.Generic;
using UnityEngine;

public class speed_adjust : MonoBehaviour
{


    private BaseCharacter baseCharacter;

    public int speed_default;

    private void Start()
    {
        speed_default = baseCharacter.speed;
    }


    public void speedx2()
    {
        //baseCharacter.SetSpeed(speed_default);
    }




    //public void AdjustSpeed_hero()
    //{
    //    // Adjust speed for all heroes
    //    foreach (GameObject hero in heroes)
    //    {
    //        HeroMovement heroMovement = hero.GetComponent<HeroMovement>();
    //        if (heroMovement != null)
    //        {
    //            heroMovement.speed = newSpeed_hero;
    //        }
    //        else
    //        {
    //            Debug.LogWarning("HeroMovement component not found on a hero!");
    //        }
    //        // Adjust speed for all enemies

    //    }



    //    Debug.Log("Speed adjusted to ");
    //}

    //public void AdjustSpeed_enemy()
    //{
    //    Debug.Log("AdjustSpeed_enemy");

    //    foreach (GameObject enemy in enemies)
    //    {
    //        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
    //        if (enemyMovement != null)
    //        {
    //            Debug.Log("-60");
    //            enemyMovement.speed = newSpeed_enemy;
    //        }
    //        else
    //        {
    //            Debug.LogWarning("EnemyMovement component not found on an enemy!");
    //        }
    //    }



    //    Debug.Log("Speed adjusted to ");
    //}

    //// Example method to add new enemies dynamically
    //public void AddEnemy(GameObject enemyPrefab)
    //{
    //    if (enemyPrefab != null)
    //    {
    //        GameObject newEnemy = Instantiate(enemyPrefab);
    //        enemies.Add(newEnemy);
    //    }
    //}
}

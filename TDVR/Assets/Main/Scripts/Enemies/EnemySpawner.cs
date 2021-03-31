using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemySmall;
    public GameObject enemyLarge;
    public GameObject enemyBoss;

    private int counter = 0;
    private int spawnType = 0;
    private float spawnDelay = 1.5f;
    private float nextSpawnTime;

    public int numberOfRounds;

    // Update is called once per frame
    void Update()
    {
        if (ShouldSpawn())
        {
            SpawnEnemy();
        }
    }

    private bool ShouldSpawn()
    {
        return Time.time > nextSpawnTime;
    }

    private void SpawnEnemy()
    {
        nextSpawnTime = Time.time + spawnDelay;

        counter++;
        if (counter % 3 == 0)
        {
            spawnType++;
        }

        if (spawnType == 0)
        {
            Instantiate(enemy, transform.position, transform.rotation);
            
        }
        else if (spawnType == 1)
        {
            Instantiate(enemySmall, transform.position, transform.rotation);
        }
        else if (spawnType == 2)
        {
            Instantiate(enemyLarge, transform.position, transform.rotation);
        }
       else if (spawnType == 3)
        {
            Instantiate(enemyBoss, transform.position, transform.rotation);
        }
    }
}

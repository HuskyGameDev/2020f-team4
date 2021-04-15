using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;

    //public Transform enemyPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    public float countdown = 2f;

    private int waveIndex = 0;
    public Canvas menuPane;
    public Canvas gameOver; //gameover canvas
    public Health health;   //Health Box

    void Start() {
        gameOver.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemiesAlive <= 0 && !(health.GetHealth() <= 0)) {    //where menu is made visible if all enemies are defeated and player is still alive
            menuPane.enabled = true;
            if (waveIndex == waves.Length) {
                menuPane.transform.GetChild(0).gameObject.SetActive(false); //finished all waves, only get exit button in the menu and no next wave button
            }
        }

        if (health.GetHealth() <= 0)    //if player DIES only give them the game over splash and the quit botton, no next round button. F.
        {
            menuPane.enabled = true;
            menuPane.transform.GetChild(0).gameObject.SetActive(false);
            gameOver.enabled = true;
        }

        // if (countdown <= 0f) {
        //     StartCoroutine(SpawnWave());
        //     countdown = timeBetweenWaves;
        //     return;
        // }
        // countdown -= Time.deltaTime;
    }

    public IEnumerator SpawnWave() {

        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveIndex++;

        if (waveIndex == waves.Length) {
            Debug.Log("LEVEL OVER");
        }
        
    }
    void SpawnEnemy(GameObject enemy) {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}

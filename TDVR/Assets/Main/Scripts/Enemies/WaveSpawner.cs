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
    private int enemiesSpawned = 0;
    private Wave wave;
    public Canvas menuPane;
    public Canvas gameOver; //gameover canvas
    public Health health;   //Health Box

    void Start() {
        gameOver.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (wave != null && enemiesSpawned == wave.count && EnemiesAlive <= 0 && !(health.GetHealth() <= 0)) {    //where menu is made visible if all enemies are defeated and player is still alive
            menuPane.enabled = true;
            if (waveIndex == waves.Length) {
                menuPane.transform.GetChild(0).gameObject.SetActive(false); //finished all waves, only get exit button in the menu and no next wave button
                menuPane.transform.GetChild(1).gameObject.SetActive(true);
            }
            FindObjectOfType<AudioManager>().Play("RoundEnd"); // ROUND END SOUND

            enemiesSpawned = 0;
        }

        if (health.GetHealth() <= 0 && enemiesSpawned > 0)    //if player DIES only give them the game over splash and the quit botton, no next round button. F.
        {
            StartCoroutine(PlaySoundDelayed("GameOver", 1f)); // GAME OVER SOUND
            StartCoroutine(PlaySoundDelayed("ChildishMusic", 2f)); // BETWEEN-ROUND MUSIC
            menuPane.enabled = true;
            menuPane.transform.GetChild(0).gameObject.SetActive(false);
            gameOver.enabled = true;
            enemiesSpawned = 0;
        }

        // if (countdown <= 0f) {
        //     StartCoroutine(SpawnWave());
        //     countdown = timeBetweenWaves;
        //     return;
        // }
        // countdown -= Time.deltaTime;
    }

    public IEnumerator SpawnWave() {

        FindObjectOfType<AudioManager>().Stop("ChildishMusic"); // ROUND START SOUND
        FindObjectOfType<AudioManager>().Play("RoundStart"); // ROUND START SOUND

        wave = waves[waveIndex];

        for (enemiesSpawned = 0; enemiesSpawned < wave.count; enemiesSpawned++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveIndex++;

        if (waveIndex == waves.Length) {
            Debug.Log("LEVEL OVER");
        }
        
    }

    public IEnumerator PlaySoundDelayed(string name, float delay) {
        yield return new WaitForSeconds(delay);
        FindObjectOfType<AudioManager>().Play(name); // GAME OVER SOUND
    }

    void SpawnEnemy(GameObject enemy) {
        EnemiesAlive++;
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        FindObjectOfType<AudioManager>().Play("EnemyGrunt_2"); // ENEMY SPAWN SOUND
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING, BREAK }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public List<Transform> enemies;
        public List<int> counts;
        public float rate;
        public float searchCountdown = 1f;
    }

    public Wave[] waves;
    private int nextWave = 0;
    private int currWave = 1;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 15f;
    public float breakTime = 15f;
    private float waveCountdown;
    private float breakCountdown;

    private SpawnState state = SpawnState.COUNTING;
    public Text waveNumberUI;

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawns");
        }
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {

        switch (state)
        {
            case SpawnState.WAITING:
                if (!EnemyIsAlive())
                {
                    WaveCompleted();
                }
                break;

            case SpawnState.COUNTING:
                waveCountdown -= Time.deltaTime;
                if (waveCountdown <= 0)
                {
                    if (nextWave < waves.Length)
                    {
                        StartCoroutine(SpawnWave(waves[nextWave]));
                    }
                    else
                    {
                        state = SpawnState.BREAK;
                        breakCountdown = breakTime;
                        Debug.Log("All Waves Complete! Taking a break...");
                    }
                }
                break;

            case SpawnState.BREAK:
                breakCountdown -= Time.deltaTime;
                if (breakCountdown <= 0)
                {
                    nextWave = 0;
                    waveCountdown = timeBetweenWaves;
                    state = SpawnState.COUNTING;
                }
                break;

            default:
                break;
        }
    }

    void WaveCompleted()
{
    Debug.Log("Wave Completed");

    if (nextWave + 1 > waves.Length - 1)
    {
        nextWave = 0;
        currWave++;
        Debug.Log("All Waves Complete! Looping....");
    }
    else
    {
        nextWave++;
        currWave++;
    }

    waveCountdown = timeBetweenWaves;
    state = SpawnState.COUNTING;

    // heal the player for 20 HP after each wave
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player != null)
    {
        Player playerScript = player.GetComponent<Player>();
        playerScript.HealPlayer(20);
    }

    waveNumberUI.text = "Wave: " + currWave;
}


    bool EnemyIsAlive()
    {
        Wave currentWave = waves[nextWave];
        currentWave.searchCountdown -= Time.deltaTime;

        if (currentWave.searchCountdown <= 0f)
        {
            currentWave.searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.enemies.Count; i++)
        {
            for (int j = 0; j < _wave.counts[i]; j++)
            {
                SpawnEnemy(_wave.enemies[i]);
                yield return new WaitForSeconds(1f / _wave.rate);
            }
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy.name);
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemyGO = Instantiate(_enemy, _sp.position, _sp.rotation).gameObject;
        EnemyRecieveDamage enemyDamage = enemyGO.GetComponent<EnemyRecieveDamage>();
        if (enemyDamage != null)
        {
            enemyDamage.stats.boostPercentage = currWave; // apply boost percentage
            enemyDamage.stats.Init();
        }
    }

}
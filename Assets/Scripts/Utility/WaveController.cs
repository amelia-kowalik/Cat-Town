using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveController : MonoBehaviour
{
    [System.Serializable] 
    public class Wave
    {
        [field: SerializeField] public GameObject[] spawners { get; private set; }
        [field: SerializeField] public int coyoteCount { get; private set; } 
        [field: SerializeField] public float timeBeforeSpawn { get; private set; }
    }

    [SerializeField] private Wave[] waves;
    [SerializeField] private GameObject coyotePrefab;
    private int currentWave = 0;
    private float timeBetweenWaves;
    private bool canSpawn = true;


    private void Awake()
    {
         timeBetweenWaves = waves[currentWave].timeBeforeSpawn;
    }
    
    private void Update()
    {
        if (!canSpawn)
        {
            return;
        }

        if (Time.time >= timeBetweenWaves)
        {
            Debug.Log("wave:" + currentWave);
            StartWave();
            NextWave();
            
            timeBetweenWaves = Time.time + waves[currentWave].timeBeforeSpawn;
        }
    }

    void StartWave()
    {
        Wave wave = waves[currentWave];
        GameObject[] spawners = wave.spawners;

        if (spawners.Length == 0)
        {
            return;
        }

        for (int i = 0; i < wave.coyoteCount; i++)
        {
            GameObject randomSpawner = spawners[Random.Range(0, spawners.Length)];
            Vector2 spawnPosition = new Vector2(randomSpawner.transform.position.x, randomSpawner.transform.position.y);
            Instantiate(coyotePrefab, spawnPosition, Quaternion.identity);
        }

    }

    public void NextWave()
    {
        if (currentWave + 1 < waves.Length)
        {
            currentWave++;
            GameManager.OnNextWave?.Invoke(currentWave);
        }
        else
        {
            canSpawn = false;
        }
    }
    
}

using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    [System.Serializable] 
    public class Wave
    {
        [field: SerializeField] public GameObject[] Spawners { get; private set; }
        [field: SerializeField] public int CoyoteCount { get; private set; } 
        [field: SerializeField] public float TimeBeforeSpawn { get; private set; }
    }

    [SerializeField] private Wave[] waves;
    [SerializeField] private GameObject bossSpawner;
    [SerializeField] private int currentWave = 0;
    private float _timeBetweenWaves;
    private bool _canSpawn = true;


    private void Awake()
    {
         _timeBetweenWaves = waves[currentWave].TimeBeforeSpawn;
    }
    
    private void Update()
    {
        if (!_canSpawn)
        {
            return;
        }

        if (Time.time >= _timeBetweenWaves)
        {
            Debug.Log("wave:" + currentWave);
            StartWave();
            NextWave();
            
            _timeBetweenWaves = Time.time + waves[currentWave].TimeBeforeSpawn;
        }
    }

    void StartWave()
    {
        Wave wave = waves[currentWave];
        GameObject[] spawners = wave.Spawners;

        if (spawners.Length == 0)
        {
            return;
        }

        if (currentWave == 11)
        {
            GameManager.Instance.SpawnManager.SpawnBoss(bossSpawner);
        }
        
        for (int i = 0; i < wave.CoyoteCount; i++)
        {
            GameObject randomSpawner = spawners[Random.Range(0, spawners.Length)];
            GameManager.Instance.SpawnManager.SpawnCoyote(randomSpawner);
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
            _canSpawn = false;
        }
    }
    
}

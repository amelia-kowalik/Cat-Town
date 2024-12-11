using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoyoteSpawnerComponent : MonoBehaviour
{
    [System.Serializable] 
    public class WaveContent
    {
        [SerializeField] private GameObject[] spawners;
        [SerializeField] private int coyoteCount;

        public GameObject[] GetSpawners()
        {
            return spawners;
        }

        public int GetCoyoteCount()
        {
            return coyoteCount;
        }
    }

    [SerializeField] private WaveContent[] waves;
    [SerializeField] private GameObject coyotePrefab;
    int currentWave = 0;

    void StartWave()
    {
        WaveContent wave = waves[currentWave];
        GameObject[] spawners = wave.GetSpawners();
        int coyoteCount = wave.GetCoyoteCount();

        if (spawners.Length == 0)
        {
            Debug.LogError("CoyoteSpawnerComponent: No Spawners found!");
            return;
        }

        for (int i = 0; i < coyoteCount; i++)
        {
            GameObject randomSpawner = spawners[Random.Range(0, spawners.Length)];
            Vector2 spawnPosition = new Vector2(randomSpawner.transform.position.x, randomSpawner.transform.position.y);
            Instantiate(coyotePrefab, spawnPosition, Quaternion.identity);
        }

    }

    public void NextWave()
    {
        currentWave++;
        if (currentWave < waves.Length)
        {
            StartWave();
        }
        else
        {
            Debug.Log("All waves completed!");
        }
    }

    private void Start()
    {
        StartWave();
    }
}

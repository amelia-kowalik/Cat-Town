using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    
    [SerializeField] private GameObject coyotePrefab;
    [SerializeField] private GameObject bossPrefab;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    public void SpawnCoyote(GameObject spawner)
    {
        Vector2 spawnPosition = new Vector2(spawner.transform.position.x, spawner.transform.position.y);
        Instantiate(coyotePrefab, spawnPosition, Quaternion.identity);
    }

    public void SpawnBoss(GameObject spawner)
    {
        Vector2 spawnPosition = new Vector2(spawner.transform.position.x, spawner.transform.position.y);
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }
}

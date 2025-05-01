using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }
    
    [SerializeField] private Cowboy cowboy;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    public bool BuyFood(int healthAdded, int cost)
    {
        if (ScoreManager.Instance.SpendGold(cost))
        {
            cowboy.Heal(healthAdded);
            return true;
        }
        
        return false;
        
    }

    public bool CanBuyFood()
    {
        return cowboy.Stats["health"] < cowboy.Stats["maxHealth"];
    }
}

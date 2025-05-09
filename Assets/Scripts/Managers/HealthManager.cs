using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Cowboy cowboy;
    
    
    public bool BuyFood(int healthAdded, int cost)
    {
        if (GameManager.Instance.ScoreManager.SpendGold(cost))
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

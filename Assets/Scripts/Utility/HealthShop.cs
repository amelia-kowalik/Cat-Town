using System.Collections.Generic;
using UnityEngine;

struct Food
{
    public string Name { get; private set; }
    public int HealthAdded { get; private set; }
    public int Cost { get; private set; }

    public Food(string name, int healthAdded, int cost)
    {
        Name = name;
        HealthAdded = healthAdded;
        Cost = cost;
    }
}

public class HealthShop
{
    private List<Food> foods;
    
    

    private void BuyFood(Food food )
    {
        if (ScoreManager.Instance.SpendGold(food.Cost))
        {
            GameManager.OnNextWave?.Invoke(food.HealthAdded);
        }
    }
    
}

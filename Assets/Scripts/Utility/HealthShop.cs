using System.Collections.Generic;
using UnityEngine;

struct Food
{
    private string name;
    private int healthAdded;
    private int cost;

    public int GetCost()
    {
        return cost;
    }
}

public class HealthShop
{
    private List<Food> foods;

    private void BuyFood(Food food )
    {
        if (ScoreManager.Instance.SpendGold(food.GetCost()))
        {
            
        }
    }
    
}

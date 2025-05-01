using System;
using UnityEngine;

public class HealthShopUI : MonoBehaviour
{
    [SerializeField] private FoodItem[] foodItems;
    [SerializeField] private GameObject shopPanel;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        foreach (FoodItem item in foodItems)
        {
            item.UpdateUI();
        }
    }
    
    public void CloseShop()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    
}

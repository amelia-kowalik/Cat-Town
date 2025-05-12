using System;
using UnityEngine;

public class HealthShopUI : MonoBehaviour
{
    [SerializeField] private HealthItem[] foodItems;
    [SerializeField] private GameObject shopPanel;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        foreach (HealthItem item in foodItems)
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

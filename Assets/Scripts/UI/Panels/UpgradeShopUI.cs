using System;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShopUI : MonoBehaviour
{
    [SerializeField] private UpgradeItem[] upgradeItems;
    [SerializeField] private GameObject shopPanel;

    void Start()
    {
        Init();
    }
    
    
    public void Init()
    {
        foreach (UpgradeItem upgradeItem in upgradeItems)
        {
            upgradeItem.UpdateUI();
        }
        
    }
    
    public void CloseShop()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}

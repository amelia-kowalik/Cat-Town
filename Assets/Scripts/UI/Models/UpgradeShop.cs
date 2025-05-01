using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    [SerializeField] private UpgradeShopUI upgradeShopUI;
    [SerializeField] private Cowboy cowboy;
    
    /*private void Start()
    {
            ScoreManager.Instance.OnGoldChanged += upgradeShopUI.UpdateGoldUI;
            upgradeShopUI.UpdateGoldUI(ScoreManager.Instance.GetGold());
    }
    
    private void OnDestroy()
    {
            ScoreManager.Instance.OnGoldChanged -= upgradeShopUI.UpdateGoldUI;
    }*/

    

    
}

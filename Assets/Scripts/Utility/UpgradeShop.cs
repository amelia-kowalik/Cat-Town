using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop
{
    UpgradeManager upgradeManager;
    private string shopName;
    private List<Upgrade> upgrades;

    private void BuyUpgrade(string upgradeName)
    {
        bool success = upgradeManager.BuyUpgrade(upgradeName);

        if (success)
        {
            //zmienic display w UI
        }
        
    }
}

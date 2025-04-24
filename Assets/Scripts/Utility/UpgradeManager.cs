using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Cowboy cowboy;
    [SerializeField] private GameObject expert;
    [SerializeField] private GameObject[] snipers;

    public List<Upgrade> upgrades = new List<Upgrade>();
    
    private void Start()
    {
        upgrades.Add(new Upgrade("Faster Walking", "Cowboy", "walkingSpeed", 3f, 2, 10));
        upgrades.Add(new Upgrade("Better Gun", "Cowboy", "baseDamage", 10f, 5, 10));
        upgrades.Add(new Upgrade("Toughen Up", "Cowboy", "health", 100f, 3, 10));
        upgrades.Add(new Upgrade("Self Defense", "Game", "", 0f, 2, 10));
        upgrades.Add(new Upgrade("Cat Cowboys", "Game", "", 0f, 3, 10));
        upgrades.Add(new Upgrade("???", "Game", "", 0f, 1, 10));
    
    ApplyAllStatUpgrades();
    }

    public void ApplyStatUpgrade(Upgrade upgrade)
    {
        if (upgrade.appliedTo == "Cowboy" && cowboy.Stats.ContainsKey(upgrade.stat))
        {
            cowboy.Stats[upgrade.stat] += upgrade.Amount;
            
            if (upgrade.stat == "maxHealth")
            {
                cowboy.Stats["health"] = Mathf.Min(cowboy.Stats["health"], cowboy.Stats["maxHealth"]);
            }
        }
    }

    public void ApplyAllStatUpgrades()
    {
        foreach (var upgrade in upgrades)
        {
            if (upgrade.appliedTo == "Cowboy" && upgrade.currentLevel > 0)
            {
                ApplyStatUpgrade(upgrade);
            }
        }
    }

    public void LevelUpUpgrade(string upgradeName)
    {
        Upgrade upgradeToLevelUp = null;
        
        foreach (var upgrade in upgrades )
        {
            if (upgrade.upgradeName == upgradeName)
            {
                upgradeToLevelUp = upgrade;
                break;
            }
        }

        if (upgradeToLevelUp != null && upgradeToLevelUp.CanLevelUp())
        {
            upgradeToLevelUp.LevelUp();
            
            if(upgradeToLevelUp.appliedTo == "Cowboy")
            {
                ApplyStatUpgrade(upgradeToLevelUp);
            }
            else
            {
                upgradeToLevelUp.upgradeAction?.Invoke();
            }
        }
        else
        {
            Debug.Log("Upgrade Failed");
        }
    }

    public bool BuyUpgrade(string upgradeName)
    {
        Upgrade upgradeToBuy = upgrades.Find(x => x.upgradeName == upgradeName);

        if (upgradeToBuy == null || !upgradeToBuy.CanLevelUp())
        {
            return false;
        }

        if (ScoreManager.Instance.SpendGold(upgradeToBuy.cost))
        {
            LevelUpUpgrade(upgradeName);
            return true;
        }

        return false;
    }

    private void TrainNpcs()
    {
        Upgrade selfDefense = upgrades.Find(x => x.upgradeName == "Self Defense");

        int chancesToPunch = selfDefense.currentLevel + 1;

        NPC[] npcs = FindObjectsByType<NPC>(FindObjectsSortMode.None);
        foreach (var npc in npcs)
        {
            npc.updateLives(chancesToPunch); 
            Debug.Log($"Updated NPC lives to {chancesToPunch}");
        }
    }

    private void TrainSniper()
    {
        Upgrade catCowboys = upgrades.Find(x => x.upgradeName == "Cat Cowboys");

        int sniperCount = catCowboys.currentLevel + 1;

        snipers[sniperCount].SetActive(true);

    }

    private void UnlockExpert()
    {
        Upgrade expertUpgrade = upgrades.Find(x => x.upgradeName == "???");
        
        
        expert.SetActive(true);
    }
    
}

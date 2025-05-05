using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }
    [SerializeField] private Cowboy cowboy;
    [SerializeField] private GameObject expert;
    [SerializeField] private GameObject[] snipers;

    public List<Upgrade> upgrades = new List<Upgrade>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    void Start()
    {
        //stat upgrades
        upgrades.Add(new Upgrade("Faster Walk", "Cowboy", "walkingSpeed", 3f, 2, 10));
        upgrades.Add(new Upgrade("Better Pistol", "Cowboy", "baseDamage", 10f, 5, 20));
        upgrades.Add(new Upgrade("Toughen Up", "Cowboy", "maxHealth", 100f, 3, 15));
        
        //other upgrades
        Upgrade selfDefenseUpgrade = new Upgrade("Self Defense", "Game", "", 0f, 3, 20);
        selfDefenseUpgrade.upgradeAction = TrainNpcs;
        upgrades.Add(selfDefenseUpgrade);
        
        Upgrade catCowboysUpgrade = new Upgrade("Cat Cowboys", "Game", "", 0f, 3, 25);
        catCowboysUpgrade.upgradeAction = TrainSniper;
        upgrades.Add(catCowboysUpgrade);
        
        Upgrade expertUpgrade =new Upgrade("Expert", "Game", "", 0f, 1, 200);
        expertUpgrade.upgradeAction = UnlockExpert;
        upgrades.Add(expertUpgrade);
        
    ApplyAllStatUpgrades();
    }

    public void ApplyStatUpgrade(Upgrade upgrade)
    {
        if (upgrade.appliedTo == "Cowboy" && cowboy.Stats.ContainsKey(upgrade.stat))
        {
            cowboy.Stats[upgrade.stat] += upgrade.Amount;
            
            if (upgrade.stat == "maxHealth")
            {
                cowboy.Stats["health"] += upgrade.Amount;
                cowboy.Stats["health"] = Mathf.Min(cowboy.Stats["health"], cowboy.Stats["maxHealth"]);
                GameManager.OnHealthChanged?.Invoke(cowboy.Stats["health"], cowboy.Stats["maxHealth"]);
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

        int chancesToPunch = selfDefense.currentLevel;

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

        int sniperCount = catCowboys.currentLevel;
        
        snipers[sniperCount].SetActive(true);

    }

    private void UnlockExpert()
    {
        Upgrade expertUpgrade = upgrades.Find(x => x.upgradeName == "Expert");
        
        
        expert.SetActive(true);
    }
    
}

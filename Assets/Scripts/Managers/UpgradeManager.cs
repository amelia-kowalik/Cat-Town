using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private const string FasterWalk = "Faster Walk";
    private const string BetterPistol = "Better Pistol";
    private const string ToughenUp = "Toughen Up";
    private const string SelfDefense = "Self Defense";
    private const string CatCowboys = "Cat Cowboys";
    private const string Expert = "Expert";
    private const string Cowboy = "Cowboy";
    private const string WalkingSpeedStat = "walkingSpeed";
    private const string BaseDamageStat = "baseDamage";
    private const string MaxHealthStat = "maxHealth";
    private const string HealthStat = "health";
    private const string Game = "Game";
    
    [SerializeField] private Cowboy cowboy;
    [SerializeField] private GameObject expert;
    [SerializeField] private GameObject[] snipers;

    public List<Upgrade> upgrades = new List<Upgrade>();
    
    
    void Start()
    {
        //stat upgrades
        upgrades.Add(new Upgrade(FasterWalk, Cowboy, WalkingSpeedStat, 3f, 2, 10));
        upgrades.Add(new Upgrade(BetterPistol, Cowboy, BaseDamageStat, 10f, 5, 20));
        upgrades.Add(new Upgrade(ToughenUp, Cowboy, MaxHealthStat, 100f, 3, 15));
        
        //other upgrades
        Upgrade selfDefenseUpgrade = new Upgrade(SelfDefense, Game, "", 0f, 3, 20);
        selfDefenseUpgrade.upgradeAction = TrainNpcs;
        upgrades.Add(selfDefenseUpgrade);
        
        Upgrade catCowboysUpgrade = new Upgrade(CatCowboys, Game, "", 0f, 3, 25);
        catCowboysUpgrade.upgradeAction = TrainSniper;
        upgrades.Add(catCowboysUpgrade);
        
        Upgrade expertUpgrade =new Upgrade(Expert, Game, "", 0f, 1, 200);
        expertUpgrade.upgradeAction = UnlockExpert;
        upgrades.Add(expertUpgrade);
        
    ApplyAllStatUpgrades();
    }

    public void ApplyStatUpgrade(Upgrade upgrade)
    {
        if (upgrade.appliedTo == Cowboy && cowboy.Stats.ContainsKey(upgrade.stat))
        {
            cowboy.Stats[upgrade.stat] += upgrade.Amount;
            
            if (upgrade.stat == MaxHealthStat)
            {
                cowboy.Stats[HealthStat] += upgrade.Amount;
                cowboy.Stats[HealthStat] = Mathf.Min(cowboy.Stats[HealthStat], cowboy.Stats[MaxHealthStat]);
                GameManager.OnHealthChanged?.Invoke(cowboy.Stats[HealthStat], cowboy.Stats[MaxHealthStat]);
            }
        }
    }

    public void ApplyAllStatUpgrades()
    {
        foreach (var upgrade in upgrades)
        {
            if (upgrade.appliedTo == Cowboy && upgrade.currentLevel > 0)
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
            
            if(upgradeToLevelUp.appliedTo == Cowboy)
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

        if (GameManager.Instance.ScoreManager.SpendGold(upgradeToBuy.cost))
        {
            LevelUpUpgrade(upgradeName);
            return true;
        }

        return false;
    }

    private void TrainNpcs()
    {
        Upgrade selfDefense = upgrades.Find(x => x.upgradeName == SelfDefense);

        int chancesToPunch = selfDefense.currentLevel;

        NPC[] npcs = FindObjectsByType<NPC>(FindObjectsSortMode.None);
        foreach (var npc in npcs)
        {
            npc.UpdateLives(chancesToPunch); 
            Debug.Log($"Updated NPC lives to {chancesToPunch}");
        }
    }

    private void TrainSniper()
    {
        Upgrade catCowboys = upgrades.Find(x => x.upgradeName == CatCowboys);

        int sniperCount = catCowboys.currentLevel;
        
        snipers[sniperCount].SetActive(true);

    }

    private void UnlockExpert()
    {
        Upgrade expertUpgrade = upgrades.Find(x => x.upgradeName == Expert);
        
        
        expert.SetActive(true);
    }
    
}

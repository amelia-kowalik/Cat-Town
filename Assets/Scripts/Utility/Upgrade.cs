using System;
using UnityEngine;

public class Upgrade
{
    public string upgradeName;
    public string appliedTo;
    public string stat;
    public float baseLevel;
    public int currentLevel;
    public int maxLevel;
    public int baseCost;
    public Action upgradeAction;

    public Upgrade(string upgradeName, string appliedTo, string stat, float baseLevel, int maxLevel, int baseCost)
    {
        this.upgradeName = upgradeName;
        this.appliedTo = appliedTo;
        this.stat = stat;
        this.baseLevel = baseLevel;
        this.currentLevel = 1;
        this.maxLevel = maxLevel;
        this.baseCost = baseCost;
    }
    public int cost
    {
        get
        {
            return Mathf.CeilToInt(baseCost * Mathf.Pow(2.3f, currentLevel - 1));
        }
    }

    public float Amount{
        get
        {
            return baseLevel * (currentLevel * 0.3f);
        }
    }

    public bool CanLevelUp()
    {
        return (currentLevel < maxLevel);
    }

    public void LevelUp()
    {
        if (CanLevelUp())
        {
            currentLevel++;
            Debug.Log($"Upgrade {upgradeName} leveled up to {currentLevel}!");

            if (appliedTo == "Game")
            {
                upgradeAction?.Invoke();
            }
        }
    }
    
}

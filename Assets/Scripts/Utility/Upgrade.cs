using UnityEngine;

public class Upgrade
{
    public string upgradeName;
    public string appliedTo;
    public string stat;
    public float baseLevel;
    public int currentLevel;
    public int maxLevel;

    public Upgrade(string upgradeName, string appliedTo, string stat, float baseLevel, int maxLevel)
    {
        this.upgradeName = upgradeName;
        this.appliedTo = appliedTo;
        this.stat = stat;
        this.baseLevel = baseLevel;
        this.currentLevel = 1;
        this.maxLevel = maxLevel;
    }

    public float Amount{
        get
        {
            return currentLevel * baseLevel;
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
        }
    }
    
}

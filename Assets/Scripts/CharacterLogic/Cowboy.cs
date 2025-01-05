using System.Collections.Generic;
using UnityEngine;

public class Cowboy : MonoBehaviour
{
    public Dictionary<string,float> Stats { get; set; }

    public void Awake()
    {
        Stats = new Dictionary<string, float>()
        {
            { "health", 100f },
            { "walkingSpeed", 3f },
            { "baseDamage", 10f }
        };
    }

    public void ApplyUpgrade(Upgrade upgrade)
    {
        if (upgrade.appliedTo == "Cowboy" && Stats.ContainsKey(upgrade.stat))
        {
            Stats[upgrade.stat] += upgrade.Amount;
        }
    }
    

    public void TakeDamage(int damage)
    {
        Stats["health"] -= damage;
    }

    public void Heal(int heal)
    {
        Stats["health"] += heal;
    }

    public void CowboyDeath()
    {
        if (Stats["health"] <= 0)
        {
            //gameover
        }
    }

    public int GetCowboyDamage()
    {
        int randomDamage = Random.Range(10, 50);
        if (randomDamage == 50)
        {
            Debug.Log("Critical Hit!!!");
        }

        return randomDamage;
    }
    
    
    
}

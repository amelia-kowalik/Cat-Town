using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public Cowboy cowboy;

    public List<Upgrade> upgrades = new List<Upgrade>()
    {
        new Upgrade("Faster Walking", "Cowboy", "walkingSpeed", 3f, 2),
        new Upgrade("Better Gun", "Cowboy", "baseDamage", 10f, 5),
        new Upgrade("Toughen Up", "Cowboy", "health", 100f, 3)
    };
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

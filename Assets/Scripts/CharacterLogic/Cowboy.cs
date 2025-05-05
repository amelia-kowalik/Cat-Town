using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cowboy : MonoBehaviour
{
    

    public Dictionary<string,float> Stats { get; set; }
    [SerializeField, ReadOnly] private float currentHealth;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        GameManager.OnFoodBought += Heal;
    }
    
    private void OnDestroy()
    {
        GameManager.OnFoodBought -= Heal;
    }

    public void Awake()
    {
        Stats = new Dictionary<string, float>()
        {
            { "health", 100f },
            { "maxHealth", 100f },
            { "walkingSpeed", 2f },
            { "baseDamage", 10f }
        };
        currentHealth = Stats["health"];
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
        animator.SetTrigger("Hurt");
        
        Stats["health"] -= damage;
        GameManager.OnHealthChanged?.Invoke(Stats["health"], Stats["maxHealth"]);
        
        if (Stats["health"] <= 0)
        {
            animator.SetBool("isDead", true);
        }
    }

    public void Heal(int heal)
    {
        Stats["health"] = Mathf.Min(Stats["health"] + heal, Stats["maxHealth"]);
        GameManager.OnHealthChanged?.Invoke(Stats["health"], Stats["maxHealth"]);
    }

    public void CowboyDeath()
    {
        GameManager.OnLostGame?.Invoke();
    }

    public int GetDamage()
    {
        int randomDamage = Random.Range(10, 50);
        if (randomDamage == 50)
        {
            Debug.Log("Critical Hit!!!");
        }

        return randomDamage;
    }
    
    
    
}

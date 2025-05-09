using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cowboy : MonoBehaviour
{
    private const string Health = "health";
    private const string MaxHealth = "maxHealth";
    private const string WalkingSpeed = "walkingSpeed";
    private const string BaseDamage = "baseDamage";
    private const string IsDead = "isDead";
    private const string Hurt = "Hurt";

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
            { Health, 100f },
            { MaxHealth, 100f },
            { WalkingSpeed, 2f },
            { BaseDamage, 10f }
        };
        currentHealth = Stats[Health];
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
        animator.SetTrigger(Hurt);
        
        Stats[Health] -= damage;
        GameManager.OnHealthChanged?.Invoke(Stats[Health], Stats[MaxHealth]);
        
        if (Stats[Health] <= 0)
        {
            animator.SetBool(IsDead, true);
        }
    }

    public void Heal(int heal)
    {
        Stats[Health] = Mathf.Min(Stats[Health] + heal, Stats[MaxHealth]);
        GameManager.OnHealthChanged?.Invoke(Stats[Health], Stats[MaxHealth]);
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

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

    public Dictionary<string,float> Stats { get; private set; }
    private Animator _animator;
    private CharacterSFXManager _sfxManager;

    void Start()
    {
        _animator = GetComponent<Animator>();
        GameManager.OnFoodBought += Heal;
    }
    
    void OnDestroy()
    {
        GameManager.OnFoodBought -= Heal;
    }

    void Awake()
    {
        Stats = new Dictionary<string, float>()
        {
            { Health, 100f },
            { MaxHealth, 100f },
            { WalkingSpeed, 2f },
            { BaseDamage, 10f }
        };
    }
    

    public void TakeDamage(float damage)
    {
        _animator.SetTrigger(Hurt);
        
        Stats[Health] -= damage;
        GameManager.OnHealthChanged?.Invoke(Stats[Health], Stats[MaxHealth]);
        
        if (Stats[Health] <= 0)
        {
            _animator.SetBool(IsDead, true);
        }
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

    public void Heal(int heal)
    {
        Stats[Health] = Mathf.Min(Stats[Health] + heal, Stats[MaxHealth]);
        GameManager.OnHealthChanged?.Invoke(Stats[Health], Stats[MaxHealth]);
    }

    public void CowboyDeath()
    {
        GameManager.OnLostGame?.Invoke();
    }

    
    
    
    
}

using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    [SerializeField] public int gold;
    [SerializeField] private int catKidnapLimit = 3;
    private int catsKidnapped;
    private bool limitReached;
    private int currentWave;
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    void OnEnable()
    {
        GameManager.OnCatKidnapped += OnCatKidnapped;
        GameManager.OnCoyoteDeath += AddGold;
        GameManager.OnNextWave += GetWave;
    }

    void OnDisable()
    {
        GameManager.OnCatKidnapped -= OnCatKidnapped;
        GameManager.OnCoyoteDeath -= AddGold;
        GameManager.OnNextWave -= GetWave;
    }


    private void AddGold()
    {
        int bonus = currentWave * 2;
        gold += bonus;
    }

    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            return true;
        }
        return false;
    }
    
    private void OnCatKidnapped()
    {
        catsKidnapped++;

        if (catsKidnapped >= catKidnapLimit)
        {
            limitReached = true;
        }
        
    }
    
    private void GetWave(int waveNumber)
    {
        currentWave = waveNumber;
    }
}

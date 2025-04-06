using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int gold;
    [SerializeField] private int catKidnapLimit = 3;
    private int catsKidnapped;
    private bool limitReached;
    private int currentWave;

    
    
    void Start()
    {
        
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
        GameManager.OnNextWave += GetWave;
    }


    private void AddGold()
    {
        int bonus = currentWave * 2;
        gold += bonus;
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

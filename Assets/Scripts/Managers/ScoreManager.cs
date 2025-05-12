using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    
    [SerializeField] private int gold;
    [SerializeField] private int catsTakenLimit = 3;
    private int _catsTaken;
    private int _currentWave;
    
    
    void Start()
    {
        GameManager.OnCatTaken += CountTakenCats;
        GameManager.OnCoyoteDeath += AddGold;
        GameManager.OnNextWave += GetWave;
    }

    void OnDestroy()
    {
        GameManager.OnCatTaken -= CountTakenCats;
        GameManager.OnCoyoteDeath -= AddGold;
        GameManager.OnNextWave -= GetWave;
    }


    private void AddGold()
    {
        int bonus = _currentWave * 2;
        gold += bonus;
        
        GameManager.OnGoldChanged?.Invoke(gold);
    }

    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            GameManager.OnGoldChanged?.Invoke(gold);
            return true;
        }
        return false;
    }
    
    
    public void CountTakenCats()
    {
        _catsTaken++;
        GameManager.OnCatsTakenChanged?.Invoke(_catsTaken,catsTakenLimit);

        if (_catsTaken >= catsTakenLimit)
        {
            GameManager.OnLostGame?.Invoke();
        }
        
    }
    
    private void GetWave(int waveNumber)
    {
        _currentWave = waveNumber;
    }
    
    public int GetGold()
    {
        return gold;
    }
}

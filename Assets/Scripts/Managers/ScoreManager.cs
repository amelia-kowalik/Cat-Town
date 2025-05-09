using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    [SerializeField] private int gold;
    [SerializeField] private int catKidnapLimit = 3;
    private int _catsKidnapped;
    private bool _limitReached;
    private int _currentWave;
    

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    void Start()
    {
        GameManager.OnCatKidnapped += OnCatKidnapped;
        GameManager.OnCoyoteDeath += AddGold;
        GameManager.OnNextWave += GetWave;
    }

    void OnDestroy()
    {
        GameManager.OnCatKidnapped -= OnCatKidnapped;
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

    public int GetGold()
    {
        return gold;
    }
    
    public void OnCatKidnapped()
    {
        _catsKidnapped++;
        GameManager.OnCatKidnappedChanged?.Invoke(_catsKidnapped,catKidnapLimit);

        if (_catsKidnapped >= catKidnapLimit)
        {
            _limitReached = true;
            GameManager.OnLostGame?.Invoke();
        }
        
    }
    
    private void GetWave(int waveNumber)
    {
        _currentWave = waveNumber;
    }
}

using System;
using UnityEngine;

public class 
    GameManager : MonoBehaviour
{
    public static Action OnCatKidnapped;
    public static Action OnCoyoteDeath;
    public static Action<int> OnNextWave;
    public static Action<int> OnFoodBought;
    public static Action<int,int> OnCatKidnappedChanged;
    public static Action OnGameStarted;
    public static Action OnGameOver;
    public static Action<StateManager.GameState> OnStateChanged;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

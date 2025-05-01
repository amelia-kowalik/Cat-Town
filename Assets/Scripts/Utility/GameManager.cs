using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action OnCatKidnapped;
    public static Action OnCoyoteDeath;
    public static Action<int> OnNextWave;
    public static Action<int> OnFoodBought;
    public static Action<int,int> OnCatKidnappedChanged;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

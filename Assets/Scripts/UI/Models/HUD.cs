using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private HUDUI hudUI;
    [SerializeField] private Cowboy cowboy;
    void Start()
    {
        GameManager.OnHealthChanged += (cur,max) => hudUI.UpdateHealth(cur, max);
        hudUI.UpdateHealth(cowboy.Stats["health"], cowboy.Stats["maxHealth"]);
        
        GameManager.OnGoldChanged += hudUI.UpdateGold;
        hudUI.UpdateGold(GameManager.Instance.ScoreManager.GetGold());

        GameManager.OnCatsTakenChanged += hudUI.UpdateKidnapCounter;
    }

    void OnDestroy()
    {
        GameManager.OnGoldChanged -= hudUI.UpdateGold;
        GameManager.OnCatsTakenChanged -= hudUI.UpdateKidnapCounter;
    }
}

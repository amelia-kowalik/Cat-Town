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
        hudUI.UpdateGold(ScoreManager.Instance.GetGold());

        GameManager.OnCatKidnappedChanged += hudUI.UpdateKidnapCounter;
    }

    void OnDestroy()
    {
        GameManager.OnGoldChanged -= hudUI.UpdateGold;
        GameManager.OnCatKidnappedChanged -= hudUI.UpdateKidnapCounter;
    }
}

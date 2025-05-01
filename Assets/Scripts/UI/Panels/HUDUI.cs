using TMPro;
using UnityEngine;

public class HUDUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthAmount;
    [SerializeField] private TextMeshProUGUI catCounter;
    [SerializeField] private TextMeshProUGUI goldAmount;
    
    public void UpdateGold(int gold)
    {
        goldAmount.text = gold.ToString();
    }

    public void UpdateHealth(float current, float max)
    {
        healthAmount.text = $"{(int)current} / {(int)max}";
    }

    public void UpdateKidnapCounter(int current, int limit)
    {
        catCounter.text = $"{current} / {limit}";
    }
}

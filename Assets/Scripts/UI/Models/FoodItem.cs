using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodItem : MonoBehaviour
{
    [SerializeField] private string foodName;
    [SerializeField] private int healthAdded; 
    [SerializeField] private int price;
    [SerializeField] private TextMeshProUGUI foodNameText;
    [SerializeField] private TextMeshProUGUI healthAddedText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button buyButton;
    
    
    void Start()
    {
       UpdateUI();
    }

    public void BuyFood()
    {
        bool success = HealthManager.Instance.BuyFood(healthAdded, price);

        if (success)
        {
            UpdateUI();
        }
        else
        {
            Debug.Log("failed to purchase");
        }
    }
    
    public void UpdateUI(){
        foodNameText.text = foodName;
        healthAddedText.text = $"+{healthAdded}";
        priceText.text = $"{price}";
        
        bool canBuy = ScoreManager.Instance.GetGold() >= price;
        bool isDamaged = HealthManager.Instance.CanBuyFood();
        
        buyButton.interactable = canBuy && isDamaged;
    }
}

using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UpgradeItem : MonoBehaviour
{
    [SerializeField] private string upgradeName;
    [SerializeField] private TextMeshProUGUI upgradeLevel;
    [SerializeField] private TextMeshProUGUI upgradeCost;
    [SerializeField] private TextMeshProUGUI upgradeDescription;
    [SerializeField] private Button buyButton;
    void OnEnable()
    {
            UpdateUI();
    }

    public void BuyUpgrade()
    {
        bool success = GameManager.Instance.UpgradeManager.BuyUpgrade(upgradeName);

        if (success)
        {
            UpdateUI();
        }
        else
        {
            Debug.Log("failed to purchase");
        }
    }
    
    public void UpdateUI()
    {
        Upgrade upgrade = GameManager.Instance.UpgradeManager.upgrades.Find(x => x.upgradeName == upgradeName);

        if (upgrade != null)
        {
            upgradeLevel.text = $"{upgrade.currentLevel} / {upgrade.maxLevel}";
            
            if (upgrade.CanLevelUp())
            {
                upgradeLevel.text = $"{upgrade.currentLevel} / {upgrade.maxLevel}";
                upgradeCost.text = $"{upgrade.cost}";
                buyButton.interactable = true;
            }
            else
            {
                upgradeLevel.text = "MAX";
                upgradeCost.text = "-";
                buyButton.interactable = false;
            }
        }
    }
}

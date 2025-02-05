using UnityEngine;
using UnityEngine.UI;

public class TowerRequirements : MonoBehaviour
{
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private BuildMaster buildMaster;
    [SerializeField] private float towerPrice;
    [SerializeField] private Button towerBuyButton;
    private void Awake()
    {
        CheckBuildRequirements();
    }
    public void CheckBuildRequirements()
    {
        if(towerBuyButton == null)
        {
            return;
        }
        if(currencyManager.totalGold >= towerPrice)
        {
            towerBuyButton.interactable = true;
        }
        else
        {
            towerBuyButton.interactable = false;
        }
    }
    public void SendTowerPrice(float TowerPrice)
    {
        buildMaster.selectedTowerPrice = TowerPrice;
    }
}

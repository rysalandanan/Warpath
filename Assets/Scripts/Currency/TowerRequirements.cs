using UnityEngine;
using UnityEngine.UI;

public class TowerRequirements : MonoBehaviour
{
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private BuildMaster buildMaster;
    [SerializeField] private float towerPrice;
    [SerializeField] private Button towerBuildButton;
    private void Awake()
    {
        CheckBuildRequirements();
    }
    public void CheckBuildRequirements()
    {
        if(towerBuildButton == null)
        {
            return;
        }
        if(currencyManager.totalGold >= towerPrice)
        {
            towerBuildButton.interactable = true;
        }
        else
        {
            towerBuildButton.interactable = false;
        }
    }
    public void SendTowerPrice(float TowerPrice)
    {
        buildMaster.selectedTowerPrice = TowerPrice;
    }
}

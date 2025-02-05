using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyCount;
    [SerializeField] private TowerRequirements[] towerRequirements;

    public float totalGold = 200;

    private void Start()
    {
        UpdateCurrencyCount();
        CheckRequirements();
    }
    public void GainGold(float amount)
    {
        totalGold += amount;
        UpdateCurrencyCount();
        CheckRequirements();
    }
    public void ReduceGold(float amount)
    {
        totalGold -= amount;
        UpdateCurrencyCount();
        CheckRequirements();
    }
    private void CheckRequirements()
    {
        for (int i = 0; i < towerRequirements.Length; i++)
        {
            towerRequirements[i].CheckBuildRequirements();
        }
    }
    private void UpdateCurrencyCount()
    {
        currencyCount.text = "Gold: " + totalGold.ToString();
    }
}

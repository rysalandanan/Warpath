using UnityEngine;
using System.Collections;

public class GoldGenerator : MonoBehaviour
{
    [SerializeField] private float goldGenerateAmount;
    private bool _isEarning = false;
    private CurrencyManager _currencyManager;

    private void Start()
    {
        _currencyManager = FindAnyObjectByType<CurrencyManager>();
    }
    private IEnumerator GenerateGold()
    {
        while (_isEarning)
        {
            if(Time.timeScale > 0)
            {
                yield return new WaitForSeconds(5);
                _currencyManager.GainGold(goldGenerateAmount);
            }
        }
    }
    public void canEarn(bool yesNo)
    {
        _isEarning = yesNo;
        StartCoroutine(GenerateGold());
    }
}

using UnityEngine;
using System.Collections;

public class TowerGold : MonoBehaviour
{
    private bool isEarning = false;
    private CurrencyManager _currencyManager;

    private void Start()
    {
        _currencyManager = FindAnyObjectByType<CurrencyManager>();
    }
    private void Awake()
    {
        isEarning = true;
        StartCoroutine(GenerateGold());
    }
    
    private IEnumerator GenerateGold()
    {
        while (isEarning)
        {
            yield return new WaitForSeconds(5);
            _currencyManager.GainGold(5);
        }
    }    
}

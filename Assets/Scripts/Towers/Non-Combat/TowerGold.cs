using UnityEngine;
using System.Collections;

public class TowerGold : MonoBehaviour
{
    [SerializeField] private float goldGenerateAmount;
    private bool _isEarning = false;
    private CurrencyManager _currencyManager;

    private void Start()
    {
        _currencyManager = FindAnyObjectByType<CurrencyManager>();
    }
    private void Awake()
    {
        _isEarning = true;
        StartCoroutine(GenerateGold());
    }

    private IEnumerator GenerateGold()
    {
        while (_isEarning)
        {
            yield return new WaitForSeconds(5);
            _currencyManager.GainGold(goldGenerateAmount);
        }
    }
}

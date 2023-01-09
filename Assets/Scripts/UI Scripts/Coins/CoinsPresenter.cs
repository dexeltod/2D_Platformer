using TMPro;
using UnityEngine;

public class CoinsPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private CoinsPackage _coinsPackage;
    private CoinTaker[] _coins;

    private int _coinsCount = 0;

    private void Awake()
    {
        _coins = _coinsPackage.GetComponentsInChildren<CoinTaker>();
    }

    private void OnEnable()
    {
        foreach (CoinTaker coinTaker in _coins)
            coinTaker.CoinTaked += IncreaseCoinCount;
    }

    private void OnDisable()
    {
        foreach (CoinTaker coinTaker in _coins)
            coinTaker.CoinTaked -= IncreaseCoinCount;
    }

    private void IncreaseCoinCount()
    {
        _coinsCount++;
        _coinsText.text = _coinsCount.ToString();
    }
}

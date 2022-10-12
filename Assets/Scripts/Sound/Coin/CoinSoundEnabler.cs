using UnityEngine;

public class CoinSoundEnabler : MonoBehaviour
{
    private CoinsPackage _coinsPackage;
    private AudioSource _audio;
    private CoinTaker[] _coins;

    private void Awake()
    {
        _coinsPackage = GetComponent<CoinsPackage>();
        _coins = _coinsPackage.GetComponentsInChildren<CoinTaker>();
        _audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        foreach (CoinTaker coinTaker in _coins)
            coinTaker.CoinTaked += PlaySound;
    }

    private void OnDisable()
    {
        foreach (CoinTaker coinTaker in _coins)
            coinTaker.CoinTaked -= PlaySound;
    }

    private void PlaySound()
    {
        _audio.Play();
    }
}

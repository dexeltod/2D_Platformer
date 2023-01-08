using UnityEngine;

[RequireComponent(typeof(CoinsPackageObject))]
[RequireComponent(typeof(AudioSource))]

public class CoinSoundEnabler : MonoBehaviour
{
    private CoinsPackageObject _coinsPackageObject;
    private CoinTaker[] _coins;
    private AudioSource _audio;

    private void Awake()
    {
        _coinsPackageObject = GetComponent<CoinsPackageObject>();
        _audio = GetComponent<AudioSource>();
        _coins = _coinsPackageObject.GetComponentsInChildren<CoinTaker>();
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

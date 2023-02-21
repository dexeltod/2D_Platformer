using Game.Environment.Items.Coin;
using UnityEngine;

namespace Game.Sound.Coin
{
    [RequireComponent(typeof(CoinsPackage))]
    [RequireComponent(typeof(AudioSource))]

    public class CoinSoundEnabler : MonoBehaviour
    {
        private CoinsPackage _coinsPackage;
        private CoinTaker[] _coins;
        private AudioSource _audio;

        private void Awake()
        {
            _coinsPackage = GetComponent<CoinsPackage>();
            _audio = GetComponent<AudioSource>();
            _coins = _coinsPackage.GetComponentsInChildren<CoinTaker>();
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
}

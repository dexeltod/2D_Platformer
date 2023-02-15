using TMPro;
using UnityEngine;

namespace Game.PlayerScripts
{
    public class PlayerMoneyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private PlayerMoney _playerMoney;

        private void OnEnable() =>
            _playerMoney.MoneyCountChanged += OnReloadMoneyCount;

        private void OnDisable()
        {
            _playerMoney.MoneyCountChanged -= OnReloadMoneyCount;
        }

        private void OnReloadMoneyCount(int money)
        {
            _moneyText.text = money.ToString();
        }
    }
}
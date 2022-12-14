using PlayerScripts.Weapons;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMoney : MonoBehaviour
{
	[SerializeField] private int _money = 4000;

	private Player _player;

	public event UnityAction<WeaponBase> PurchaseCompleted;
	public event UnityAction<int> MoneyCountChanged;

	private void Awake() =>
		_player = GetComponent<Player>();

	private void OnEnable() =>
		_player.Bought += OnTrySpendMoney;

	private void OnDisable() =>
		_player.Bought -= OnTrySpendMoney;

	private void Start() =>
		Initialize();

	private void OnTrySpendMoney(int price, WeaponBase weaponBase)
	{
		const int MinMoneyValue = 0;

		if (_money < MinMoneyValue)
			return;

		_money -= price;
		PurchaseCompleted?.Invoke(weaponBase);
		MoneyCountChanged?.Invoke(_money);
	}

	private void Initialize() =>
		MoneyCountChanged?.Invoke(_money);
}
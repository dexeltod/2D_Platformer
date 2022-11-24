using System.Collections.Generic;
using PlayerScripts.Weapons;
using UI_Scripts.Shop;
using UnityEngine;

public class Shop : MonoBehaviour
{
	[SerializeField] private ShopItemView _weaponPanelPrefab;
	[SerializeField] private List<ItemInfo> _items;
	[SerializeField] private Player _player;

	private void Awake() =>
		Initialize();

	private void Initialize()
	{
		foreach (var itemInfo in _items)
		{
			var currentItem = Instantiate(_weaponPanelPrefab, transform);
			currentItem.Render(itemInfo.WeaponBase, itemInfo, itemInfo.Sprite);
			currentItem.BuyButtonClicked += OnBuyButtonClick;
		}
	}

	private void OnBuyButtonClick(WeaponBase weaponBase, ItemInfo itemInfo, ShopItemView shopItemView)
	{
		_player.TryBuyWeapon(weaponBase, itemInfo);
		shopItemView.BuyButtonClicked -= OnBuyButtonClick;
	}
}
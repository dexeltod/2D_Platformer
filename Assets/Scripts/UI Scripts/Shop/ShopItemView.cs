using TMPro;
using UI_Scripts.Shop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour
{
	[SerializeField] private Image _image;
	[SerializeField] private TextMeshProUGUI _description;
	[SerializeField] private TextMeshProUGUI _title;
	[SerializeField] private TextMeshProUGUI _price;
	[SerializeField] private Button _button;

	private WeaponBase _weaponBase;
	private ItemInfo _itemInfo;

	public event UnityAction<WeaponBase, ItemInfo, ShopItemView> BuyButtonClicked;

	~ShopItemView()
	{
		_button.onClick.RemoveListener(OnBuy);
	}
	
	public void Render(WeaponBase weaponBase, ItemInfo itemInfo, Sprite image)
	{
		_weaponBase = weaponBase;
		_itemInfo = itemInfo;
		_title.text = itemInfo.Title;
		_description.text = itemInfo.Description;
		_price.text = itemInfo.Price.ToString();
		_image.sprite = image;

		_button.onClick.AddListener(OnBuy);
	}

	private void TryLockItem()
	{
		if (_weaponBase.IsBought == false)
		{
			_button.interactable = false;
			_weaponBase.SetBoughtStateTrue();
		}
	}

	private void OnBuy()
	{
		BuyButtonClicked?.Invoke(_weaponBase, _itemInfo, this);
		TryLockItem();
		_button.onClick.RemoveListener(OnBuy);
	}
}
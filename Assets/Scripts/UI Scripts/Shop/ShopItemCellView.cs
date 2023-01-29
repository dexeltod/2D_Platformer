using Game.PlayerScripts.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI_Scripts.Shop
{
    public class ShopItemCellView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private Button _button;

        private AbstractWeapon _abstractWeapon;
        private Item _item;

        public event UnityAction<Item, ShopItemCellView> BuyButtonClicked;

        ~ShopItemCellView() =>
            _button.onClick.RemoveListener(OnBuy);

        public void Render(AbstractWeapon weaponBase, Item item, Sprite image)
        {
            _abstractWeapon = weaponBase;
            _item = item;
            _title.text = item.Title;
            _description.text = item.Description;
            _price.text = item.Price.ToString();
            _image.sprite = image;

            _button.onClick.AddListener(OnBuy);
        }

        private void TryLockItem()
        {
            if (_abstractWeapon.IsBought == false)
            {
                _button.interactable = false;
                _abstractWeapon.SetBoughtStateTrue();
            }
        }

        private void OnBuy()
        {
            BuyButtonClicked?.Invoke(_item, this);
            TryLockItem();
            _button.onClick.RemoveListener(OnBuy);
        }
    }
}
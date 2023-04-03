using Game.PlayerScripts.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace View.UI_Scripts.Shop
{
    public class ShopItemCellView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private Button _button;

        private AbstractWeapon _abstractWeapon;
        private ItemScriptableObject _itemScriptableObject;

        public event UnityAction<ItemScriptableObject, ShopItemCellView> BuyButtonClicked;

        ~ShopItemCellView() =>
            _button.onClick.RemoveListener(OnBuy);

        public void Render(ItemScriptableObject itemScriptableObject, Sprite image)
        {
            _itemScriptableObject = itemScriptableObject;
            _title.text = itemScriptableObject.Title;
            _description.text = itemScriptableObject.Description;
            _price.text = itemScriptableObject.Price.ToString();
            _image.sprite = image;

            _button.onClick.AddListener(OnBuy);
        }

        private void TryLockItem()
        {
            if (_itemScriptableObject.IsBought == false)
            {
                _button.interactable = false;
                _itemScriptableObject.SetBought(true);
            }
        }

        private void OnBuy()
        {
            BuyButtonClicked?.Invoke(_itemScriptableObject, this);
            TryLockItem();
            _button.onClick.RemoveListener(OnBuy);
        }
    }
}
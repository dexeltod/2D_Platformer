using System;
using System.Collections.Generic;
using Game.PlayerScripts;
using Infrastructure.Data.PersistentProgress;
using Infrastructure.Data.Serializable;
using Infrastructure.GameLoading;
using UnityEngine;

namespace View.UI_Scripts.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private ShopItemCellView _weaponPanelPrefab;
        [SerializeField] private List<ItemScriptableObject> _items;
        [SerializeField] private Player _player;

        private GameProgress _gameProgress;

        private void Awake()
        {
            IPersistentProgressService persistentProgressService = ServiceLocator.Container.GetSingle<IPersistentProgressService>();
            _gameProgress = persistentProgressService.GameProgress;
        
            Initialize();
        }

        public void Reload(GameProgress progress)
        {
	        // progress.PlayerProgressData.PlayerItemsData.AddWeaponInData(new ItemScriptableObject());
        }
        
        private void Initialize()
        {
	        if (_items == null)
		        throw new NullReferenceException("Shop does not have items");

	        foreach (var itemInfo in _items)
            {
                var currentItem = Instantiate(_weaponPanelPrefab, transform);
                currentItem.Render(itemInfo, itemInfo.Sprite);
                currentItem.BuyButtonClicked += OnBuyButtonClick;
            }
        }

        private void OnBuyButtonClick(ItemScriptableObject itemScriptableObject, ShopItemCellView shopItemView)
        {
            _player.TryBuyWeapon(itemScriptableObject);
            shopItemView.BuyButtonClicked -= OnBuyButtonClick;
            Reload(_gameProgress);
        }

        
    }
}
using System;
using System.Collections.Generic;
using Game.PlayerScripts;
using Infrastructure.Data;
using Infrastructure.Data.PersistentProgress;
using Infrastructure.GameLoading;
using UnityEngine;

namespace UI_Scripts.Shop
{
    public class Shop : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private ShopItemCellView _weaponPanelPrefab;
        [SerializeField] private List<Item> _items;
        [SerializeField] private Player _player;

        private GameProgress _gameProgress;

        private void Awake()
        {
            IPersistentProgressService persistentProgressService = ServiceLocator.Container.GetSingle<IPersistentProgressService>();
            _gameProgress = persistentProgressService.GameProgress;
        
            Initialize();
        }

        private void Initialize()
        {
            foreach (var itemInfo in _items)
            {
                var currentItem = Instantiate(_weaponPanelPrefab, transform);
                currentItem.Render(itemInfo.Prefab, itemInfo, itemInfo.Sprite);
                currentItem.BuyButtonClicked += OnBuyButtonClick;
            }
        }

        private void OnBuyButtonClick(Item item, ShopItemCellView shopItemView)
        {
            _player.TryBuyWeapon(item);
            shopItemView.BuyButtonClicked -= OnBuyButtonClick;
            Update(_gameProgress);
        }

        public void Update(GameProgress progress)
        {
            progress.ItemsData.UpdateWeaponData(new Item());
        }

        public void Load(GameProgress progress)
        {
            throw new NotImplementedException();
        }
    }
}
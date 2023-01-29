using System;
using System.Collections.Generic;
using System.Linq;
using Game.Animation.AnimationHashes.Characters;
using Infrastructure.Data;
using Infrastructure.Data.PersistentProgress;
using Infrastructure.GameLoading;
using UI_Scripts.Shop;
using UnityEngine;

namespace Game.PlayerScripts
{
    public class PlayerWeaponList
    {
        private readonly Transform _transform;
        private readonly Weapons.WeaponFactory _weaponFactory;
        private readonly PlayerMoney _playerMoney;

        private List<Item> _items;
        private Weapons.AbstractWeapon _equippedWeapon;
        private AnimationHasher _animationHasher;

        public event Action<Weapons.AbstractWeapon> EquippedWeaponChanged;

        public PlayerWeaponList(Weapons.WeaponFactory weaponFactory,
            PlayerMoney playerMoney, Transform transform)
        {
            GameProgress gameProgress = ServiceLocator
                .Container
                .GetSingle<IPersistentProgressService>()
                .GameProgress;
            
            _items = gameProgress.ItemsData.GetBoughtItems();
            
            _weaponFactory = weaponFactory;
            _playerMoney = playerMoney;
            _transform = transform;
            _playerMoney.PurchaseCompleted += OnAddBoughtWeapon;
            SetStartWeapon();
        }

        ~PlayerWeaponList() =>
            _playerMoney.PurchaseCompleted -= OnAddBoughtWeapon;

        public Weapons.AbstractWeapon GetEquippedWeapon() =>
            _equippedWeapon;

        private void OnAddBoughtWeapon(Item weaponBase)
        {
            weaponBase.SetBought(true);
            _items.Add(weaponBase);
        }

        private void SetStartWeapon()
        {
            if (_items.Count <= 0)
                return;

            Item item = _items.FirstOrDefault();

            Weapons.AbstractWeapon initializedAbstractWeapon = GetInitializedWeapon(item.Prefab);
            EquippedWeaponChanged?.Invoke(initializedAbstractWeapon);
        }

        private Weapons.AbstractWeapon GetInitializedWeapon(Weapons.AbstractWeapon firstAbstractWeapon)
        {
            Weapons.AbstractWeapon abstractWeapon = _weaponFactory.CreateWeapon(firstAbstractWeapon, _transform);
            return abstractWeapon;
        }

        private void SetWeapon(Weapons.AbstractWeapon weaponBase)
        {
            _equippedWeapon = weaponBase;
            EquippedWeaponChanged?.Invoke(_equippedWeapon);
        }
    }
}
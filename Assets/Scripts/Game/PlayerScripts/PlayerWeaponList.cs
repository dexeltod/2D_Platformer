using System;
using System.Collections.Generic;
using System.Linq;
using Game.Animation.AnimationHashes.Characters;
using Game.PlayerScripts.Weapons;
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
        private readonly WeaponFactory _weaponFactory;
        private readonly PlayerMoney _playerMoney;

        private readonly List<ItemScriptableObject> _items;
        private AbstractWeapon _equippedWeapon;
        private AnimationHasher _animationHasher;

        public event Action<AbstractWeapon> EquippedWeaponChanged;

        public PlayerWeaponList(WeaponFactory weaponFactory,
            PlayerMoney playerMoney, Transform transform)
        {
            GameProgress gameProgress = ServiceLocator
                .Container
                .GetSingle<IPersistentProgressService>()
                .GameProgress;
            
            _items = gameProgress.PlayerItemsData.GetBoughtItems();
            
            _weaponFactory = weaponFactory;
            _playerMoney = playerMoney;
            _transform = transform;
            
            SetStartWeapon();
        }

        public AbstractWeapon GetEquippedWeapon() =>
            _equippedWeapon;

        private void OnAddBoughtWeapon(ItemScriptableObject weaponBase)
        {
            weaponBase.SetBought(true);
            _items.Add(weaponBase);
        }

        private void SetStartWeapon()
        {
	        ItemScriptableObject itemScriptableObject = _items.FirstOrDefault();

            AbstractWeapon initializedAbstractWeapon = GetInitializedWeapon(itemScriptableObject.Prefab.GetComponent<AbstractWeapon>());
            
            SetWeapon(initializedAbstractWeapon);
        }

        private AbstractWeapon GetInitializedWeapon(AbstractWeapon firstAbstractWeapon)
        {
            AbstractWeapon abstractWeapon = _weaponFactory.CreateWeapon(firstAbstractWeapon, _transform);
            return abstractWeapon;
        }

        private void SetWeapon(AbstractWeapon weaponBase)
        {
            _equippedWeapon = weaponBase;
            EquippedWeaponChanged?.Invoke(_equippedWeapon);
        }
    }
}
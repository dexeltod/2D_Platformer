using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Animation.AnimationHashes.Characters;
using Game.PlayerScripts.Weapons;
using Infrastructure.Data;
using Infrastructure.Data.PersistentProgress;
using Infrastructure.GameLoading;
using UnityEngine;
using View.UI_Scripts.Shop;

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
	        PlayerMoney playerMoney, Transform transform, List<ItemScriptableObject> items)
        {
	        _items = items;
	        _weaponFactory = weaponFactory;
            _playerMoney = playerMoney;
            _transform = transform;
        }

        public AbstractWeapon GetEquippedWeapon() =>
            _equippedWeapon;

        private void OnAddBoughtWeapon(ItemScriptableObject weaponBase)
        {
            weaponBase.SetBought(true);
            _items.Add(weaponBase);
        }

        public async UniTask SetStartWeapon()
        {
	        ItemScriptableObject itemScriptableObject = _items.FirstOrDefault();

	        if (itemScriptableObject != null)
	        {
		        AbstractWeapon initializedAbstractWeapon = await GetInitializedWeapon(itemScriptableObject.Prefab.GetComponent<AbstractWeapon>());
            
		        SetWeapon(initializedAbstractWeapon);
	        }
        }

        private async UniTask<AbstractWeapon> GetInitializedWeapon(AbstractWeapon firstAbstractWeapon)
        {
            AbstractWeapon abstractWeapon =  await _weaponFactory.CreateWeapon(firstAbstractWeapon, _transform);
            return abstractWeapon;
        }

        private void SetWeapon(AbstractWeapon weaponBase)
        {
            _equippedWeapon = weaponBase;
            EquippedWeaponChanged?.Invoke(_equippedWeapon);
        }
    }
}
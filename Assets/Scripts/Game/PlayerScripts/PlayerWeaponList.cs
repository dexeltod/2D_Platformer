using System;
using System.Collections.Generic;
using System.Linq;
using PlayerScripts.Weapons;
using UnityEngine;

namespace PlayerScripts
{
	[RequireComponent(typeof(Animator), typeof(WeaponFactory), typeof(PlayerMoney))]
	[RequireComponent(typeof(PlayerWeaponInventory), typeof(AnimationHasher))]
	public class PlayerWeaponInventory : MonoBehaviour
	{
		[SerializeField] private List<AbstractWeapon> _weapons;

		public event Action<AbstractWeapon> WeaponChanged;

		private WeaponFactory _weaponFactory;
		private PlayerMoney _playerMoney;
		private Animator _animator;
		private AnimationHasher _animationHasher;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
			_animationHasher = GetComponent<AnimationHasher>();
			_weaponFactory = GetComponent<WeaponFactory>();
			_playerMoney = GetComponent<PlayerMoney>();
		}

		private void OnEnable() =>
			_playerMoney.PurchaseCompleted += AddBoughtWeapon;

		private void Start() =>
			SetStartWeapon();

		private void OnDisable() =>
			_playerMoney.PurchaseCompleted -= AddBoughtWeapon;

		private void SetLastBoughtWeapon()
		{
			AbstractWeapon lastAbstractWeapon = _weapons.Last();
			AbstractWeapon initializedAbstractWeapon = GetInitializedWeapon(lastAbstractWeapon);
			SetWeapon(initializedAbstractWeapon);
		}

		private void SetStartWeapon()
		{
			if (_weapons.Count <= 0)
				throw new NullReferenceException();

			AbstractWeapon firstAbstractWeapon = _weapons.FirstOrDefault();

			AbstractWeapon initializedAbstractWeapon = GetInitializedWeapon(firstAbstractWeapon);
			WeaponChanged?.Invoke(initializedAbstractWeapon);
		}

		private AbstractWeapon GetInitializedWeapon(AbstractWeapon firstAbstractWeapon)
		{
			AbstractWeapon abstractWeapon = _weaponFactory.CreateWeapon(firstAbstractWeapon, transform);
			return abstractWeapon;
		}

		private void AddBoughtWeapon(AbstractWeapon weaponBase)
		{
			weaponBase.SetBoughtStateTrue();
			_weapons.Add(weaponBase);
			SetLastBoughtWeapon();
		}

		private void SetWeapon(AbstractWeapon weaponBase) =>
			WeaponChanged?.Invoke(weaponBase);
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayerScripts
{
	[RequireComponent(typeof(Animator), typeof(WeaponFactory),
		typeof(PlayerMoney))]
	[RequireComponent(typeof(PlayerWeapon), typeof(AnimationHasher))]
	public class PlayerWeapon : MonoBehaviour
	{
		[SerializeField] private List<WeaponBase> _weapons;

		public event Action<WeaponBase> WeaponChanged;

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
			WeaponBase lastWeapon = _weapons.Last();
			WeaponBase initializedWeapon = GetInitializedWeapon(lastWeapon);
			SetWeapon(initializedWeapon);
		}

		private void SetStartWeapon()
		{
			if (_weapons.Count <= 0)
				throw new NullReferenceException();

			WeaponBase firstWeapon = _weapons.FirstOrDefault();

			WeaponBase initializedWeapon = GetInitializedWeapon(firstWeapon);
			WeaponChanged?.Invoke(initializedWeapon);
		}

		private WeaponBase GetInitializedWeapon(WeaponBase firstWeaponBase)
		{
			WeaponBase weapon = _weaponFactory.CreateWeapon(firstWeaponBase, transform, _animator, _animationHasher);
			weapon.Initialize(_animator, _animationHasher);
			return weapon;
		}

		private void AddBoughtWeapon(WeaponBase weaponBase)
		{
			weaponBase.SetBoughtStateTrue();
			_weapons.Add(weaponBase);
			SetLastBoughtWeapon();
		}

		private void SetWeapon(WeaponBase weaponBase) =>
			WeaponChanged?.Invoke(weaponBase);
	}
}
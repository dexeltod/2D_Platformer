using System;
using System.Collections;
using UnityEngine;

namespace PlayerScripts.States
{
	public class PlayerAttackState : BaseState
	{
		private WeaponBase _currentWeapon;

		private const int LayerIndex = 0;

		private readonly PlayerWeapon _playerWeapon;
		private IStateSwitcher _stateSwitcher;
		private Coroutine _currentCoroutine;
		private Animator _animator;
		private AnimationHasher _animationHasher;

		public PlayerAttackState(Player player, IStateSwitcher stateSwitcher, AnimationHasher animationHasher,
			Animator animator, PlayerWeapon playerWeapon) : base(player, stateSwitcher, animationHasher,
			animator)
		{
			_playerWeapon = playerWeapon;
			_playerWeapon.WeaponChanged += OnWeaponSwitch;
		}

		~PlayerAttackState() =>
			_playerWeapon.WeaponChanged -= OnWeaponSwitch;

		public override void Start()
		{
			if (_currentWeapon.CanAttack == false)
				return;

			_currentCoroutine = Player.StartCoroutine(Attack());
		}

		private IEnumerator Attack()
		{
			if (_currentWeapon == null)
				yield break;

			Player.StartCoroutine(_currentWeapon.AttackRoutine(Player.LookDirection));

			AnimatorStateInfo animatorInfo = GetAnimatorInfo();

			while (animatorInfo.shortNameHash != AnimationHasher.AttackHash)
			{
				animatorInfo = GetAnimatorInfo();
				yield return null;
			}

			var waitForSeconds = new WaitForSeconds(animatorInfo.length);
			yield return waitForSeconds;
			StateSwitcher.SwitchState<PlayerIdleState>();
		}

		public override void Stop()
		{
			if (_currentCoroutine == null)
				return;

			Player.StopCoroutine(_currentCoroutine);
			_currentCoroutine = null;
		}

		private AnimatorStateInfo GetAnimatorInfo() =>
			Animator.GetCurrentAnimatorStateInfo(LayerIndex);

		private void OnWeaponSwitch(WeaponBase weaponBase) =>
			_currentWeapon = weaponBase;
	}
}
using System;
using System.Collections;
using UnityEngine;

namespace PlayerScripts.States
{
	public class PlayerAttackState : BaseState
	{
		private const int LayerIndex = 0;

		private WeaponBase _currentWeapon;

		private readonly PlayerWeapon _playerWeapon;
		private readonly PhysicsMovement _physicsMovement;

		private Animator _animator;
		private AnimationHasher _animationHasher;
		private IStateSwitcher _stateSwitcher;

		public PlayerAttackState(Player player, IStateSwitcher stateSwitcher, AnimationHasher animationHasher,
			Animator animator, PlayerWeapon playerWeapon, PhysicsMovement physicsMovement) : base(player, stateSwitcher,
			animationHasher,
			animator)
		{
			_physicsMovement = physicsMovement;
			_playerWeapon = playerWeapon;
			_playerWeapon.WeaponChanged += OnWeaponSwitch;
		}

		~PlayerAttackState() =>
			_playerWeapon.WeaponChanged -= OnWeaponSwitch;

		public override void Start()
		{
			if (_currentWeapon.CanAttack == false)
				return;

			Player.StartCoroutine(Attack());
		}

		private IEnumerator Attack()
		{
			if (_currentWeapon == null)
				yield break;

			Player.StartCoroutine(_currentWeapon.AttackRoutine(Player.LookDirection));

			AnimatorStateInfo animatorInfo = GetAnimatorInfo();

			var waitingTime = new WaitForSeconds(animatorInfo.length);
			yield return waitingTime;
			ChooseTransition();
		}

		private void ChooseTransition()
		{
			const float MinVerticalOffset = 1;

			if (_physicsMovement.Offset.x > MinVerticalOffset)
				StateSwitcher.SwitchState<PlayerRunState>();
			else
				StateSwitcher.SwitchState<PlayerIdleState>();
		}

		public override void Stop()
		{
			
		}

		private AnimatorStateInfo GetAnimatorInfo() =>
			Animator.GetCurrentAnimatorStateInfo(LayerIndex);

		private void OnWeaponSwitch(WeaponBase weaponBase) =>
			_currentWeapon = weaponBase;
	}
}
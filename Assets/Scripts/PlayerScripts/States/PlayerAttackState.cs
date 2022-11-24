using System.Collections;
using PlayerScripts.Weapons;
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
			if (_currentWeapon == null || _currentWeapon.CanAttack == false)
				yield break;
			
			AnimatorStateInfo animatorInfo = GetAnimatorInfo();
			_currentWeapon.SetGroundedBool(_physicsMovement.IsGrounded);
			_currentWeapon.SetRunBool(_physicsMovement.Offset);
			Player.StartCoroutine(_currentWeapon.AttackRoutine(Player.LookDirection));


			var waitingTime = new WaitForSeconds(animatorInfo.length * animatorInfo.speedMultiplier);
			yield return waitingTime;
			ChooseTransition();
		}

		private void ChooseTransition()
		{
			const float MinVerticalOffset = 0;

			if (_physicsMovement.Offset.x != MinVerticalOffset)
				StateSwitcher.SwitchState<PlayerRunState>();
			else if (_physicsMovement.Offset.y < 0)
				StateSwitcher.SwitchState<PlayerFallState>();
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
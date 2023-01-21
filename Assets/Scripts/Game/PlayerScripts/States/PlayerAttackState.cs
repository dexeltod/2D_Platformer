using PlayerScripts.Weapons;
using UnityEngine;

namespace PlayerScripts.States
{
	public class PlayerAttackState : PlayerStateMachine
	{
		private AbstractWeapon _currentAbstractWeapon;

		private readonly PlayerWeapon _playerWeapon;
		private readonly PhysicsMovement _physicsMovement;

		private Animator _animator;
		private AnimationHasher _animationHasher;

		public PlayerAttackState(Player player, IPlayerStateSwitcher stateSwitcher, AnimationHasher animationHasher,
			Animator animator, PlayerWeapon playerWeapon, PhysicsMovement physicsMovement) : base(player, stateSwitcher,
			animationHasher,
			animator)
		{
			_physicsMovement = physicsMovement;
			_playerWeapon = playerWeapon;
			_playerWeapon.WeaponChanged += OnWeaponSwitch;
		}

		~PlayerAttackState()
		{
			_playerWeapon.WeaponChanged -= OnWeaponSwitch;
		}

		public override void Start()
		{
			if (_currentAbstractWeapon.CanAttack == false)
				return;
			
			Attack();
		}

		public override void Stop()
		{
		}

		private void Attack()
		{
			if (_currentAbstractWeapon == null || _currentAbstractWeapon.CanAttack == false)
				return;
			
			_currentAbstractWeapon.SetGroundedBool(_physicsMovement.IsGrounded);
			_currentAbstractWeapon.SetRunBool(_physicsMovement.Offset);
			Player.StartCoroutine(_currentAbstractWeapon.AttackRoutine(Player.LookDirection));
		}

		private void OnWeaponSwitch(AbstractWeapon weaponBase) =>
			_currentAbstractWeapon = weaponBase;
	}
}
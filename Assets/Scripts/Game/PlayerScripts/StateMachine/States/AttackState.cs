using Game.Animation.AnimationHashes.Characters;
using Infrastructure.Services;
using UnityEngine;

namespace Game.PlayerScripts.StateMachine.States
{
	public class AttackState : State
	{
		private readonly Move.PhysicsMovement _physicsMovement;

		private Weapons.AbstractWeapon _currentWeapon;

		public AttackState(IInputService inputService, PlayerWeaponList playerWeaponList, Weapons.AbstractWeapon weaponBase,
			Animator animator, AnimationHasher hasher,
			Move.PhysicsMovement physicsMovement, IStateTransition[] transitions) : base(inputService, animator, hasher,
			transitions)
		{
			playerWeaponList.EquippedWeaponChanged += SetEquippedWeapon;
			_currentWeapon = weaponBase;
			_physicsMovement = physicsMovement;
		}

		protected override void OnEnter()
		{
			Attack();
		}

		protected override void OnExit()
		{
		}

		private void SetEquippedWeapon(Weapons.AbstractWeapon weapon)
		{
			_currentWeapon = weapon;
		}

		private void Attack()
		{
			bool isRun = _physicsMovement.MovementDirection.x != 0;
			
			_currentWeapon.SetRunBool(isRun);
			_currentWeapon.SetGroundedBool(_physicsMovement.IsGrounded);
			
			_currentWeapon.Use();
		}
	}
}
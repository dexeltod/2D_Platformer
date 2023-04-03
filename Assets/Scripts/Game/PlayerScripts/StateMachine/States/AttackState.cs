using Game.Animation.AnimationHashes.Characters;
using Game.PlayerScripts.Move;
using Game.PlayerScripts.Weapons;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Game.PlayerScripts.StateMachine.States
{
	public sealed class AttackState : State
	{
		private readonly PhysicsMovement _physicsMovement;

		private AbstractWeapon _currentWeapon;

		public AttackState(IInputService inputService, PlayerWeaponList playerWeaponList, AbstractWeapon weaponBase,
			Animator animator, AnimationHasher hasher,
			PhysicsMovement physicsMovement, IStateTransition[] transitions) : base(inputService, animator, hasher,
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

		private void SetEquippedWeapon(AbstractWeapon weapon)
		{
			_currentWeapon = weapon;
		}

		private void Attack()
		{
			if (_currentWeapon != null)
			{
				bool isRun = _physicsMovement.MovementDirection.x != 0;
				_currentWeapon.SetRunBool(isRun);
				_currentWeapon.Use();
			}
		}
	}
}
using Infrastructure.Services;
using PlayerScripts.Weapons;
using UnityEngine;

namespace PlayerScripts.TestStateMachine
{
	public class AttackState : State
	{
		private readonly PlayerWeapon _playerWeapon;
		private readonly AbstractWeapon _abstractWeapon;
		private readonly PhysicsMovement _physicsMovement;

		public AttackState(IInputService inputService, PlayerWeapon playerWeapon, AbstractWeapon weaponBase, Animator animator, AnimationHasher hasher,
			PhysicsMovement physicsMovement, IStateTransition[] transitions) : base(inputService, animator, hasher, transitions)
		{
			_playerWeapon = playerWeapon;
			_abstractWeapon = weaponBase;
			_physicsMovement = physicsMovement;
		}

		protected override void OnEnter()
		{
			SetAttackAnimation();
		}

		protected override void OnExit()
		{
			Animator.StopPlayback();
		}

		private void SetAttackAnimation()
		{
			Animator.Play(_physicsMovement.MovementDirection.x != 0
				? AnimationHasher.HandAttackRunHash
				: AnimationHasher.HandAttackHash);
		}
	}
}
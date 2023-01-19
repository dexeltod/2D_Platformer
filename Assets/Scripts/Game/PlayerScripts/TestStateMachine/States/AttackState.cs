using Infrastructure.Services;
using UnityEngine;

namespace PlayerScripts.TestStateMachine
{
	public class AttackState : TestState
	{
		private readonly PhysicsMovement _physicsMovement;

		public AttackState(IInputService inputService, Animator animator, AnimationHasher hasher,
			PhysicsMovement physicsMovement, ITestTransition[] transitions = null) : base(inputService, animator, hasher, transitions)
		{
			_physicsMovement = physicsMovement;
		}

		protected override void OnEnter()
		{
			SetAttackAnimation();
		}

		private void SetAttackAnimation()
		{
			if (_physicsMovement.MovementDirection.x != 0)
				Animator.Play(AnimationHasher.HandAttackRunHash);
			else
				Animator.Play(AnimationHasher.HandAttackHash);
		}

		protected override void OnExit()
		{
			Animator.StopPlayback();
		}
	}
}
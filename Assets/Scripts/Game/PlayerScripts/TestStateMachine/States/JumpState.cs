using Infrastructure.Services;
using UnityEngine;

namespace PlayerScripts.TestStateMachine
{
	public class JumpState : State
	{
		private readonly PhysicsMovement _physicsMovement;

		public JumpState(PhysicsMovement physicsMovement, IInputService inputService, Animator animator,
			AnimationHasher hasher, IStateTransition[] transitions)
			: base(inputService, animator, hasher, transitions)
		{
			_physicsMovement = physicsMovement;
		}

		protected override void OnEnter()
		{
			base.OnEnter();
			_physicsMovement.Jump();
			Animator.Play(AnimationHasher.JumpHash);
		}
	}
}
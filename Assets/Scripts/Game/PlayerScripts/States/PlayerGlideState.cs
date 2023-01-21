using Infrastructure.Services;
using PlayerScripts.TestStateMachine;
using UnityEngine;

namespace PlayerScripts.States
{
	public class PlayerGlideState : State
	{
		private readonly Animator _animator;
		private readonly AnimationHasher _animationHasher;
		private readonly PhysicsMovement _physicsMovement;
		private readonly GroundChecker _groundChecker;

		public PlayerGlideState(IInputService inputService, Animator animator, AnimationHasher hasher,
			IStateTransition[] transitions, PhysicsMovement physicsMovement, GroundChecker groundChecker) : base(inputService, animator, hasher,
			transitions)
		{
			_physicsMovement = physicsMovement;
			_groundChecker = groundChecker;
		}

		protected override void OnEnter()
		{
			_groundChecker.GroundedStateSwitched += SetNextState;
			_animator.Play(_animationHasher.GlideHash);
		}

		protected override void OnExit()
		{
			_groundChecker.GroundedStateSwitched -= SetNextState;
		}

		private void SetNextState(bool isGrounded)
		{
			const float MinVerticalOffset = 0;
		}
	}
}
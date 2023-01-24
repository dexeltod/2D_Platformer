using Infrastructure.Services;
using UnityEngine;

namespace PlayerScripts.TestStateMachine
{
	public class FallState : State
	{
		private readonly PhysicsMovement _physicsMovement;

		public FallState(IInputService inputService, Animator animator, AnimationHasher hasher,
			IStateTransition[] transitions, PhysicsMovement physicsMovement) : base(inputService, animator, hasher,
			transitions)
		{
			_physicsMovement = physicsMovement;
			InputService.VerticalButtonCanceled += OnButtonCanceled;
		}

		~FallState()
		{
			InputService.VerticalButtonCanceled -= OnButtonCanceled;
		}

		protected override void OnEnter()
		{
			base.OnEnter();

			Animator.SetBool(AnimationHasher.FallHash, true);
		}

		protected override void OnExit()
		{
			base.OnExit();

			Animator.SetBool(AnimationHasher.FallHash, false);
		}

		private void OnButtonUsed(float direction)
		{
			_physicsMovement.SetMoveDirection(direction);
		}

		private void OnButtonCanceled()
		{
			_physicsMovement.SetMoveDirection(0);
		}
	}
}
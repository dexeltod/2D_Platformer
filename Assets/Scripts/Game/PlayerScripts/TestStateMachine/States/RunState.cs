using Infrastructure.Services;
using UnityEngine;

namespace PlayerScripts.TestStateMachine
{
	public class RunState : State
	{
		private readonly PhysicsMovement _physicsMovement;
		private Vector2 _direction;

		public RunState(IInputService inputService, PhysicsMovement physicsMovement, Animator animator,
			AnimationHasher hasher, IStateTransition[] transitions) : base(inputService, animator, hasher, transitions)
		{
			_physicsMovement = physicsMovement;
			InputService.VerticalButtonUsed += SetMoveDirection;
		}

		~RunState()
		{
			InputService.VerticalButtonUsed -= SetMoveDirection;
		}

		protected override void OnEnter()
		{
			base.OnEnter();
			Animator.SetBool(AnimationHasher.RunHash, true);
		}

		protected override void OnExit()
		{
			base.OnExit();
			Animator.SetBool(AnimationHasher.RunHash, false);
		}

		private void SetMoveDirection(float direction)
		{
			_physicsMovement.SetMoveDirection(direction);
		}
	}
}
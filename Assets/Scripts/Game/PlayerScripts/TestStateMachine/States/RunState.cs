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
			Animator.Play(AnimationHasher.RunHash);
			Debug.Log("run animation");
		}

		protected override void OnExit()
		{
			base.OnExit();
		}

		private void SetMoveDirection(float direction)
		{
			Debug.Log($"set move direction {direction}");
			_physicsMovement.SetMoveDirection(direction);
		}
	}
}
using Game.Animation.AnimationHashes.Characters;
using Game.PlayerScripts.Move;
using Infrastructure.Services;
using UnityEngine;

namespace Game.PlayerScripts.StateMachine.States
{
	public class RunState : State
	{
		private readonly PhysicsMovement _physicsMovement;

		public RunState(IInputService inputService, PhysicsMovement physicsMovement, Animator animator,
			AnimationHasher hasher, IStateTransition[] transitions) : base(inputService, animator, hasher, transitions)
		{
			_physicsMovement = physicsMovement;
			InputService.VerticalButtonUsed += SetMoveDirection;
			InputService.VerticalButtonCanceled += SetFalseRunBool;
		}

		~RunState()
		{
			InputService.VerticalButtonUsed -= SetMoveDirection;
			InputService.VerticalButtonCanceled -= SetFalseRunBool;
		}

		protected override void OnEnter()
		{
			Debug.Log("RunState");
			Animator.SetBool(AnimationHasher.RunHash, true);
		}

		private void SetFalseRunBool()
		{
			Animator.SetBool(AnimationHasher.RunHash, false);
		}
		
		private void SetMoveDirection(float direction)
		{
			if (direction != 0)
				_physicsMovement.SetMoveDirection(direction);
		}
	}
}
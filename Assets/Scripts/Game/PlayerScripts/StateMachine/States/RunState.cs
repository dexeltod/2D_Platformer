using Game.Animation.AnimationHashes.Characters;
using Game.PlayerScripts.Move;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Game.PlayerScripts.StateMachine.States
{
	public sealed class RunState : State
	{
		private readonly PhysicsMovement _physicsMovement;
		private readonly AnimatorFacade _animatorFacade;

		public RunState(IInputService inputService, Animator animator, PhysicsMovement physicsMovement,
			AnimationHasher hasher, AnimatorFacade animatorFacade, IStateTransition[] transitions) : base(inputService,
			animator, hasher, transitions)
		{
			_physicsMovement = physicsMovement;
			_animatorFacade = animatorFacade;
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
			if (_animatorFacade != null)
				_animatorFacade.Play(AnimationHasher.RacingHash);
		}

		private void SetFalseRunBool()
		{
		}

		private void SetMoveDirection(float direction)
		{
			if (direction != 0 && _physicsMovement != null)
				_physicsMovement.SetMoveDirection(direction);
		}
	}
}
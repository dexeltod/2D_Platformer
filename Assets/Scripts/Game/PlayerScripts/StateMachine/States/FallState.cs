using Game.Animation.AnimationHashes.Characters;
using Game.PlayerScripts.Move;
using Infrastructure.Services;
using UnityEngine;

namespace Game.PlayerScripts.StateMachine.States
{
	public sealed  class FallState : State
	{
		private readonly AnimatorFacade _animatorFacade;
		private readonly PhysicsMovement _physicsMovement;

		public FallState(IInputService inputService, Animator animator, AnimationHasher hasher,
			AnimatorFacade animatorFacade,
			IStateTransition[] transitions, PhysicsMovement physicsMovement) : base(inputService, animator, hasher,
			transitions)
		{
			_animatorFacade = animatorFacade;
			_physicsMovement = physicsMovement;
			InputService.VerticalButtonCanceled += OnButtonCanceled;
		}

		~FallState()
		{
			InputService.VerticalButtonCanceled -= OnButtonCanceled;
		}

		protected override void OnEnter()
		{
			_animatorFacade.Play(AnimationHasher.FallHash);
		}

		private void OnButtonCanceled() => 
			_physicsMovement.SetMoveDirection(0);
	}
}
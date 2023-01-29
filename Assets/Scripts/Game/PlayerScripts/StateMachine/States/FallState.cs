using Game.Animation.AnimationHashes.Characters;
using Infrastructure.Services;
using UnityEngine;

namespace Game.PlayerScripts.StateMachine.States
{
	public class FallState : State
	{
		private readonly Move.PhysicsMovement _physicsMovement;

		public FallState(IInputService inputService, Animator animator, AnimationHasher hasher,
			IStateTransition[] transitions, Move.PhysicsMovement physicsMovement) : base(inputService, animator, hasher,
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

		private void OnButtonCanceled()
		{
			_physicsMovement.SetMoveDirection(0);
		}
	}
}
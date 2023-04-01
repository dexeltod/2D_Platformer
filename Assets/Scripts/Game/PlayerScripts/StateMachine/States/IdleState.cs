using Game.Animation.AnimationHashes.Characters;
using Game.PlayerScripts.Move;
using Infrastructure.Services;
using UnityEngine;

namespace Game.PlayerScripts.StateMachine.States
{
	public sealed class IdleState : State
	{
		private readonly PhysicsMovement _physicsMovement;
		private readonly AnimatorFacade _animatorFacade;

		public IdleState(IInputService inputService, PhysicsMovement physicsMovement, Animator animator,
			AnimationHasher hasher,
			AnimatorFacade animatorFacade,
			IStateTransition[] transitions) : base(inputService, animator, hasher, transitions)
		{
			_physicsMovement = physicsMovement;
			_animatorFacade = animatorFacade;
		}

		protected override void OnEnter()
		{
			if (_animatorFacade != null)
				_animatorFacade.Play(AnimationHasher.IdleHash);

			if (_physicsMovement != null)
				_physicsMovement.SetMoveDirection(0);
		}
	}
}
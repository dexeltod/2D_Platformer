using Game.Animation.AnimationHashes.Characters;
using Game.PlayerScripts.Move;
using Infrastructure.Services;
using UnityEngine;

namespace Game.PlayerScripts.StateMachine.States
{
	public sealed class JumpState : State
	{
		private readonly PhysicsMovement _physicsMovement;
		private readonly AnimatorFacade _animatorFacade;

		public JumpState(PhysicsMovement physicsMovement, IInputService inputService, Animator animator,
			AnimationHasher hasher, AnimatorFacade animatorFacade, IStateTransition[] transitions)
			: base(inputService, animator, hasher, transitions)
		{
			_physicsMovement = physicsMovement;
			_animatorFacade = animatorFacade;
		}

		protected override void OnEnter()
		{
			_physicsMovement.Jump();
			_animatorFacade.Play(AnimationHasher.JumpHash);
		}
	}
}
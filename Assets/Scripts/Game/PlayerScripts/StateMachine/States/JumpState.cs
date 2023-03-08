using Game.Animation.AnimationHashes.Characters;
using Game.PlayerScripts.Move;
using Infrastructure.Services;
using UnityEngine;

namespace Game.PlayerScripts.StateMachine.States
{
	public sealed class JumpState : State
	{
		private readonly PhysicsMovement _physicsMovement;
		private readonly WallCheckTrigger _wallCheckTrigger;
		private readonly AnimatorFacade _animatorFacade;

		public JumpState(PhysicsMovement physicsMovement, IInputService inputService, WallCheckTrigger wallCheckTrigger, Animator animator,
			AnimationHasher hasher, AnimatorFacade animatorFacade, IStateTransition[] transitions)
			: base(inputService, animator, hasher, transitions)
		{
			_physicsMovement = physicsMovement;
			_wallCheckTrigger = wallCheckTrigger;
			_animatorFacade = animatorFacade;
		}

		protected override void OnEnter()
		{
			_animatorFacade.Play(AnimationHasher.JumpHash);
			
			if (_wallCheckTrigger.IsWallTouched)
			{
				_physicsMovement.DoWallJump();
				return;
			}
			
			_physicsMovement.Jump();
		}
	}
}
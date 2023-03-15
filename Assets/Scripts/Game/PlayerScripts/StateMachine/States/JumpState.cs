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
		private readonly GroundChecker _groundChecker;

		public JumpState(PhysicsMovement physicsMovement, IInputService inputService, WallCheckTrigger wallCheckTrigger,
			Animator animator,
			AnimationHasher hasher, AnimatorFacade animatorFacade, GroundChecker groundChecker,
			IStateTransition[] transitions)
			: base(inputService, animator, hasher, transitions)
		{
			_physicsMovement = physicsMovement;
			_wallCheckTrigger = wallCheckTrigger;
			_animatorFacade = animatorFacade;
			_groundChecker = groundChecker;
		}

		protected override void OnEnter()
		{
			if (_animatorFacade != null)
				_animatorFacade.Play(AnimationHasher.JumpHash);

			if (_wallCheckTrigger.IsWallTouched && _groundChecker.IsGrounded == false)
			{
				_physicsMovement.DoWallJump();
				return;
			}

			if (_physicsMovement != null)
				_physicsMovement.Jump();
		}
	}
}
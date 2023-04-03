using Game.Animation.AnimationHashes.Characters;
using Game.PlayerScripts.Move;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Game.PlayerScripts.StateMachine.States
{
	public class WallSlideState : State
	{
		private const string IsWallSlide = "isWallSlide";
		private readonly AnimatorFacade _animatorFacade;
		private readonly PhysicsMovement _physicsMovement;

		public WallSlideState(IInputService inputService, Animator animator, AnimationHasher hasher,
			IStateTransition[] transitions, AnimatorFacade animatorFacade, PhysicsMovement physicsMovement) : base(inputService, animator, hasher,
			transitions)
		{
			_animatorFacade = animatorFacade;
			_physicsMovement = physicsMovement;
		}

		protected override void OnEnter()
		{
			_animatorFacade.Play(Animator.StringToHash(IsWallSlide));
			_physicsMovement.StartWallSliding();
		}
		protected override void OnExit()
		{
		}
		
	}
}
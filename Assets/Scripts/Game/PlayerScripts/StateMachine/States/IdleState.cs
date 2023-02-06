using Game.Animation.AnimationHashes.Characters;
using Infrastructure.Services;
using UnityEngine;

namespace Game.PlayerScripts.StateMachine.States
{
	public class IdleState : State
	{
		private readonly Move.PhysicsMovement _physicsMovement;

		public IdleState(IInputService inputService, Move.PhysicsMovement physicsMovement, Animator animator,
			AnimationHasher hasher,
			IStateTransition[] transitions) : base(inputService, animator, hasher, transitions)
		{
			_physicsMovement = physicsMovement;
		}

		protected override void OnEnter()
		{
			Animator.SetBool(AnimationHasher.IdleHash, true);
			_physicsMovement.SetMoveDirection(0);
		}

		protected override void OnExit()
		{
			base.OnExit();
			Animator.SetBool(AnimationHasher.IdleHash, false);
		}
	}
}
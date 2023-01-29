using Infrastructure.Services;
using UnityEngine;

namespace Game.PlayerScripts.TestStateMachine.States
{
	public class IdleState : State
	{
		private readonly PhysicsMovement _physicsMovement;

		public IdleState(IInputService inputService, PhysicsMovement physicsMovement, Animator animator,
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
using Game.Animation.AnimationHashes.Characters;
using Infrastructure.Services;
using UnityEngine;

namespace Game.PlayerScripts.StateMachine.States
{
	public class JumpState : State
	{
		private readonly Move.PhysicsMovement _physicsMovement;

		public JumpState(Move.PhysicsMovement physicsMovement, IInputService inputService, Animator animator,
			AnimationHasher hasher, IStateTransition[] transitions)
			: base(inputService, animator, hasher, transitions)
		{
			_physicsMovement = physicsMovement;
		}

		protected override void OnEnter()
		{
			_physicsMovement.Jump();
			Animator.SetBool(AnimationHasher.JumpHash, true);
		}

		protected override void OnExit()
		{
			Animator.SetBool(AnimationHasher.JumpHash, false);
		}
	}
}
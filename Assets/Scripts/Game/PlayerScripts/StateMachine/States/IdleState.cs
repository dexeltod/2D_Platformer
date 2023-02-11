using Game.Animation.AnimationHashes.Characters;
using Game.PlayerScripts.Move;
using Infrastructure.Services;
using UnityEngine;

namespace Game.PlayerScripts.StateMachine.States
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
			Debug.Log("IdleState");
			Animator.SetBool(AnimationHasher.IdleHash, true);
			_physicsMovement.SetMoveDirection(0);
		}

		protected override void OnExit()
		{
			Animator.SetBool(AnimationHasher.IdleHash, false);
		}
	}
}
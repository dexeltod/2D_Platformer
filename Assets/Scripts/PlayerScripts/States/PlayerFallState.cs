using UnityEngine;

namespace PlayerScripts.States
{
	public class PlayerFallState : BaseState
	{
		private readonly Animator _animator;
		private readonly AnimationHasher _animationHasher;
		private readonly PhysicsMovement _physicsMovement;
		private readonly IStateSwitcher _stateSwitcher;

		public PlayerFallState(Player player, IStateSwitcher stateSwitcher, AnimationHasher animationHasher,
			Animator animator, PhysicsMovement physicsMovement) : base(player, stateSwitcher, animationHasher, animator)
		{
			_stateSwitcher = stateSwitcher;
			_physicsMovement = physicsMovement;
			_animationHasher = animationHasher;
			_animator = animator;
		}

		public override void Start()
		{
			_physicsMovement.Grounded += SetNextState;
			_animator.Play(_animationHasher.FallHash);
		}

		private void SetNextState()
		{
			float minOffset = 0;

			if (_physicsMovement.MovementDirection.x != minOffset)
				_stateSwitcher.SwitchState<PlayerRunState>();
			else if (_physicsMovement.MovementDirection == Vector2.zero)
				_stateSwitcher.SwitchState<PlayerIdleState>();
		}

		public override void Stop()
		{
			_physicsMovement.Grounded -= SetNextState;
		}
	}
}
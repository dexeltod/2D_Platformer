using UnityEngine;

namespace PlayerScripts.States
{
	public class PlayerFallState : PlayerStateMachine
	{
		private readonly Animator _animator;
		private readonly AnimationHasher _animationHasher;
		private readonly PhysicsMovement _physicsMovement;
		private readonly IPlayerStateSwitcher _stateSwitcher;

		public PlayerFallState(Player player, IPlayerStateSwitcher stateSwitcher, AnimationHasher animationHasher,
			Animator animator, PhysicsMovement physicsMovement) : base(player, stateSwitcher, animationHasher, animator)
		{
			_stateSwitcher = stateSwitcher;
			_physicsMovement = physicsMovement;
			_animationHasher = animationHasher;
			_animator = animator;
		}

		public override void Start()
		{
			_animator.Play(_animationHasher.FallHash);
			_physicsMovement.Grounded += SetNextState;
		}

		private void SetNextState()
		{
			const float MinVerticalOffset = 0;

			if (_physicsMovement.Offset.x != MinVerticalOffset)
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
using UnityEngine;

namespace PlayerScripts.States
{
	public class PlayerGlideState : BaseState
	{
		private readonly Animator _animator;
		private readonly AnimationHasher _animationHasher;
		private readonly IStateSwitcher _stateSwitcher;
		private readonly PhysicsMovement _physicsMovement;

		public PlayerGlideState(Player player, IStateSwitcher stateSwitcher, AnimationHasher animationHasher,
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
			_animator.Play(_animationHasher.GlideHash);
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
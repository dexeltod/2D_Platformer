using UnityEngine;

namespace PlayerScripts.States
{
	public class PlayerJumpState : BaseState
	{
		private readonly Animator _animator;
		private readonly AnimationHasher _animationHasher;
		private readonly PhysicsMovement _physicsMovement;
		private readonly IStateSwitcher _stateSwitcher;

		public PlayerJumpState(Player player, IStateSwitcher stateSwitcher, AnimationHasher animationHasher,
			Animator animator, PhysicsMovement physicsMovement) : base(player, stateSwitcher, animationHasher, animator)
		{
			_stateSwitcher = stateSwitcher;
			_physicsMovement = physicsMovement;
			_animationHasher = animationHasher;
			_animator = animator;
		}

		public override void Start()
		{
			_physicsMovement.Fallen += SetFallState;
			_animator.Play(_animationHasher.JumpHash);
		}

		private void SetFallState()
		{
			_stateSwitcher.SwitchState<PlayerFallState>();
		}

		public override void Stop()
		{
			_physicsMovement.Fallen -= SetFallState;
		}
	}
}
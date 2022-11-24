using UnityEngine;

namespace PlayerScripts.States
{
	public class PlayerJumpState : BaseState
	{
		private readonly Animator _animator;
		private readonly AnimationHasher _animationHasher;
		private readonly PhysicsMovement _physicsMovement;

		public PlayerJumpState(Player player, IStateSwitcher stateSwitcher, AnimationHasher animationHasher,
			Animator animator, PhysicsMovement physicsMovement) : base(player, stateSwitcher, animationHasher, animator)
		{
			_physicsMovement = physicsMovement;
			_animationHasher = animationHasher;
			_animator = animator;
		}

		public override void Start()
		{
			_animator.Play(_animationHasher.JumpHash);
			_physicsMovement.Fallen += SetFallState;
		}

		private void SetFallState()
		{
			StateSwitcher.SwitchState<PlayerFallState>();
		}

		public override void Stop()
		{
			_physicsMovement.Fallen -= SetFallState;
		}
	}
}
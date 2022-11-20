using UnityEngine;

namespace PlayerScripts.States
{
	public class PlayerJumpState : BaseState
	{
		private readonly Animator _animator;
		private readonly AnimationHasher _animationHasher;
		
		public PlayerJumpState(Player player, IStateSwitcher stateSwitcher, AnimationHasher animationHasher, Animator animator) : base(player, stateSwitcher, animationHasher, animator)
		{
			_animationHasher = animationHasher;
			_animator = animator;
		}

		public override void Start()
		{
			_animator.Play(_animationHasher.JumpHash);
		}

		public override void Stop()
		{
			
		}
	}
}
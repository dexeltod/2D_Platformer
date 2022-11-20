using UnityEngine;

namespace PlayerScripts.States
{
	public class PlayerGlideState : BaseState
	{
		private readonly Animator _animator;
		private readonly AnimationHasher _animationHasher;
		private readonly IStateSwitcher _stateSwitcher;

		public PlayerGlideState(Player player, IStateSwitcher stateSwitcher, AnimationHasher animationHasher,
			Animator animator) : base(player, stateSwitcher, animationHasher, animator)
		{
			_animationHasher = animationHasher;
			_animator = animator;
		}

		public override void Start()
		{
			_animator.Play(_animationHasher.GlideHash);
		}

		public override void Stop()
		{
		}
	}
}
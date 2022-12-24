using UnityEngine;

namespace PlayerScripts
{
	public abstract class PlayerStateMachine
	{
		protected readonly Player Player;
		protected readonly IPlayerStateSwitcher StateSwitcher;
		protected readonly AnimationHasher AnimationHasher;
		protected readonly Animator Animator;

		protected PlayerStateMachine(Player player, IPlayerStateSwitcher stateSwitcher, AnimationHasher animationHasher,
			Animator animator)
		{
			Player = player;
			AnimationHasher = animationHasher;
			Animator = animator;
			StateSwitcher = stateSwitcher;
		}

		public abstract void Start();
		public abstract void Stop();
	}
}
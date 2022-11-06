using UnityEngine;

namespace PlayerScripts
{
	public abstract class BaseState
	{
		protected readonly Player Player;
		protected IStateSwitcher StateSwitcher;
		protected AnimationHasher AnimationHasher;
		protected Animator Animator;

		protected BaseState(Player player, IStateSwitcher stateSwitcher, AnimationHasher animationHasher,
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
using Infrastructure.Services;
using UnityEngine;

namespace PlayerScripts.TestStateMachine
{
	public class IdleState : TestState
	{
		public IdleState(IInputService inputService, Animator animator, AnimationHasher hasher,
			ITestTransition[] transitions) : base(inputService, animator, hasher, transitions)
		{
		}

		protected override void OnEnter()
		{
			base.OnEnter();
			Animator.Play(AnimationHasher.IdleHash);
		}
	}
}
using Infrastructure.Services;
using UnityEngine;

namespace PlayerScripts.TestStateMachine
{
	public class RunState : TestState
	{
		public RunState(IInputService inputService, Animator animator, AnimationHasher hasher,ITestTransition[] transitions = null) : base(inputService, animator, hasher, transitions)
		{
		}

		protected override void OnEnter()
		{
			base.OnEnter();
			Animator.Play(AnimationHasher.RunHash);
		}
	}
}
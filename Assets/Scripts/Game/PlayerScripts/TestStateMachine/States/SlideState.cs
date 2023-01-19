using Infrastructure.Services;
using UnityEngine;

namespace PlayerScripts.TestStateMachine
{
	public class SlideState : TestState
	{
		public SlideState(IInputService inputService, Animator animator, AnimationHasher hasher, ITestTransition[] transitions) : base(inputService, animator, hasher, transitions)
		{
		}
	}
}
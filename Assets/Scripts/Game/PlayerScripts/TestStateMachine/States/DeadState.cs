using Infrastructure.Services;
using UnityEngine;

namespace PlayerScripts.TestStateMachine
{
	public class DeadState : TestState
	{
		public DeadState(IInputService inputService, Animator animator, AnimationHasher hasher,ITestTransition[] transitions = null) : base(inputService, animator, hasher, transitions)
		{
		}
	}
}
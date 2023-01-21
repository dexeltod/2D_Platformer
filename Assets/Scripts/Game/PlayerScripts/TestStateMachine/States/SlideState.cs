using Infrastructure.Services;
using UnityEngine;

namespace PlayerScripts.TestStateMachine
{
	public class SlideState : State
	{
		public SlideState(IInputService inputService, Animator animator, AnimationHasher hasher, IStateTransition[] transitions) : base(inputService, animator, hasher, transitions)
		{
		}
	}
}
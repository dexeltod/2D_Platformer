using Infrastructure.Services;
using UnityEngine;

namespace PlayerScripts.TestStateMachine
{
	public class FallState : State
	{
		public FallState(IInputService inputService, Animator animator, AnimationHasher hasher, IStateTransition[] transitions) : base(inputService, animator, hasher, transitions)
		{
		}

		protected override void OnEnter()
		{
			base.OnEnter();
			Animator.Play(AnimationHasher.FallHash);
		}

		protected override void OnExit()
		{
			base.OnExit();
		}
	}
}
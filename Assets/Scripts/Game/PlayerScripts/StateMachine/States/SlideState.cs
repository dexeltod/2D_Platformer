using Infrastructure.Services;
using UnityEngine;

namespace Game.PlayerScripts.TestStateMachine.States
{
	public class SlideState : State
	{
		public SlideState(IInputService inputService, Animator animator, AnimationHasher hasher, IStateTransition[] transitions) : base(inputService, animator, hasher, transitions)
		{
		}
	}
}
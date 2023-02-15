using Game.Animation.AnimationHashes.Characters;
using Infrastructure.Services;
using UnityEngine;

namespace Game.PlayerScripts.StateMachine.States
{
	public class SlideState : State
	{
		public SlideState(IInputService inputService, Animator animator, AnimationHasher hasher, IStateTransition[] transitions) : base(inputService, animator, hasher, transitions)
		{
		}
	}
}
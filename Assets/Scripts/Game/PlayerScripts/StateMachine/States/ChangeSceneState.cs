using Game.Animation.AnimationHashes.Characters;
using Infrastructure.Services;
using UnityEngine;

namespace Game.PlayerScripts.StateMachine.States
{
	public class ChangeSceneState : State
	{

		public ChangeSceneState(IInputService inputService, Animator animator, AnimationHasher hasher, IStateTransition[] transitions) : base(inputService, animator, hasher, transitions)
		{
		}

		protected override void OnEnter()
		{
			
		}
	}
}
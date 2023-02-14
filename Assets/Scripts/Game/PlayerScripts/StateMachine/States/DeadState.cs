﻿using Game.Animation.AnimationHashes.Characters;
using Infrastructure.Services;
using UnityEngine;

namespace Game.PlayerScripts.StateMachine.States
{
	public class DeadState : State
	{
		public DeadState(IInputService inputService, Animator animator, AnimationHasher hasher,IStateTransition[] transitions = null) : base(inputService, animator, hasher, transitions)
		{
		}

		protected override void OnEnter()
		{
			base.OnEnter();
			Animator.Play(AnimationHasher.DieHash);
		}
	}
}
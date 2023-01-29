using System;
using Infrastructure.Services;
using UnityEngine;

namespace Game.PlayerScripts.TestStateMachine
{
	public abstract class State : IState
	{
		protected readonly Animator Animator;
		protected readonly AnimationHasher AnimationHasher;
		protected readonly IInputService InputService;

		private readonly IStateTransition[] _transitions;
		private int _currentAnimationHash;

		public State(IInputService inputService, Animator animator, AnimationHasher hasher,
			IStateTransition[] transitions)
		{
			InputService = inputService;
			Animator = animator;
			AnimationHasher = hasher;
			_transitions = transitions;
		}

		public event Action<IState> StateChanged;

		public void Enter()
		{
			OnEnter();

			foreach (IStateTransition transition in _transitions)
				transition.StateChanged += OnStateChanging;

			foreach (var transition in _transitions)
				transition.Enable();
		}

		public void Exit()
		{
			foreach (IStateTransition transition in _transitions)
				transition.StateChanged -= OnStateChanging;

			foreach (var transition in _transitions)
				transition.Disable();

			OnExit();
		}

		protected virtual void OnEnter()
		{
		}

		protected virtual void OnExit()
		{
		}

		private void OnStateChanging(IState state) =>
			StateChanged?.Invoke(state);
	}
}
using System;
using Infrastructure.Services;
using UnityEngine;

namespace PlayerScripts.TestStateMachine
{
	public abstract class TestState : ITestState
	{
		protected readonly Animator Animator;
		protected readonly AnimationHasher AnimationHasher;
		protected IInputService InputService;
		
		private readonly ITestTransition[] _transitions;


		public TestState(IInputService inputService, Animator animator, AnimationHasher hasher, ITestTransition[] transitions)
		{
			InputService = inputService;
			Animator = animator;
			AnimationHasher = hasher;
			_transitions = transitions;
		}

		public event Action<ITestState> StateChanged;

		public void Enter()
		{
			OnEnter();

			foreach (ITestTransition transition in _transitions) 
				transition.StateChanged += OnStateChanging;

			foreach (var transition in _transitions) 
				transition.Enable();
		}

		public void Exit()
		{
			foreach (ITestTransition transition in _transitions) 
				transition.StateChanged -= OnStateChanging;

			foreach (var transition in _transitions) 
				transition.Disable();
			
			OnExit();
		}

		private void OnStateChanging(ITestState state) =>
			StateChanged?.Invoke(state);
		
		protected virtual void OnEnter(){}
		protected virtual void OnExit(){}
	}
}
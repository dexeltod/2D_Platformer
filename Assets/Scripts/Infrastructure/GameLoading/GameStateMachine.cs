using System;
using System.Collections.Generic;
using UI_Scripts.Curtain;

namespace Infrastructure
{
	public interface IGameStateMachine : IService
	{
		void Enter<TState>() where TState : class, IState;
		void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
	}

	public class GameStateMachine : IGameStateMachine
	{
		private readonly Dictionary<Type, IExitState> _states;
		private IExitState _activeState;

		public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, ServiceLocator serviceLocator)
		{

			_states = new Dictionary<Type, IExitState>
			{
				[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, serviceLocator),
				[typeof(SceneLoadState)] = new SceneLoadState(this, sceneLoader, loadingCurtain, serviceLocator.Single<IGameFactory>()),
				[typeof(GameLoopState)] = new GameLoopState(this),
			};
		}

		public void Enter<TState>() where TState : class, IState
		{
			var state = ChangeState<TState>();
			state.Enter();
		}

		public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
		{
			TState state = ChangeState<TState>();
			state.Enter(payload);
		}

		private TState ChangeState<TState>() where TState : class, IExitState
		{
			_activeState?.Exit();
			TState state = GetState<TState>();
			_activeState = state;
			return state;
		}

		private TState GetState<TState>() where TState : class, IExitState =>
			_states[typeof(TState)] as TState;
	}
}
using System;
using System.Collections.Generic;
using Infrastructure.Data.PersistentProgress;
using Infrastructure.GameLoading.Factory;
using Infrastructure.Services.SaveLoadService;
using Infrastructure.States;
using UI_Scripts.Curtain;

namespace Infrastructure.GameLoading
{
	public class GameStateMachine : IGameStateMachine
	{
		private readonly Dictionary<Type, IExitState> _states;
		private IExitState _activeState;

		public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, ServiceLocator serviceLocator)
		{

			_states = new Dictionary<Type, IExitState>
			{
				[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, serviceLocator),
				
				[typeof(SceneLoadState)] = new SceneLoadState(this, sceneLoader, loadingCurtain,
					serviceLocator.GetSingle<IPlayerFactory>(),
					serviceLocator.GetSingle<IUIFactory>(),
					serviceLocator.GetSingle<ISceneLoadInformer>()
					),
				
				[typeof(LoadProgressState)] = new LoadProgressState(this,
					serviceLocator.GetSingle<IPersistentProgressService>(),
					serviceLocator.GetSingle<ISaveLoadService>()),
				
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
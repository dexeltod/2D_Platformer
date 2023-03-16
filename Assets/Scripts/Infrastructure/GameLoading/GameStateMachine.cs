using System;
using System.Collections.Generic;
using Infrastructure.Data.PersistentProgress;
using Infrastructure.GameLoading.Factory;
using Infrastructure.Services;
using Infrastructure.Services.SaveLoadService;
using Infrastructure.States;
using UI_Scripts.Curtain;

namespace Infrastructure.GameLoading
{
	public class GameStateMachine : IGameStateMachine
	{
		private readonly SoundSetter _soundSetter;
		private readonly ISoundService _soundService;
		private readonly Dictionary<Type, IExitState> _states;

		private string _currentMusicName;
		private IExitState _activeState;

		public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
			ServiceLocator serviceLocator, SoundSetter soundSetter)
		{
			_soundSetter = soundSetter;
			_states = new Dictionary<Type, IExitState>
			{
				[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, serviceLocator, _soundSetter),

				[typeof(LoadProgressState)] = new LoadProgressState(this,
					serviceLocator.GetSingle<IPersistentProgressService>(),
					serviceLocator.GetSingle<ISaveLoadService>()),

				[typeof(MenuState)] = new MenuState(sceneLoader, loadingCurtain),

				[typeof(SceneLoadState)] = new SceneLoadState(this, sceneLoader, loadingCurtain,
					serviceLocator.GetSingle<IPlayerFactory>(),
					serviceLocator.GetSingle<IUIFactory>(),
					serviceLocator.GetSingle<ISceneLoad>()),


				[typeof(GameLoopState)] = new GameLoopState(this),
			};

			_soundService = serviceLocator.GetSingle<ISoundService>();
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

		public void Enter<TState, TPayload, T>(TPayload payload, bool isLevelNameIsStopMusicBetweenScenes,
			string musicName) where TState : class, IPayloadState<TPayload>
		{
			TState state = ChangeState<TState>();
			state.Enter(payload);

			if (musicName == _currentMusicName || string.IsNullOrWhiteSpace(musicName) == true)
				return;

			if (isLevelNameIsStopMusicBetweenScenes)
				_soundService.Stop();

			_soundService.Set(musicName);
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
using System;
using Infrastructure.Constants;
using Infrastructure.GameLoading;
using Infrastructure.GameLoading.Factory;
using UI_Scripts.Curtain;
using UnityEngine;

namespace Infrastructure.States
{
	public class SceneLoadState : IPayloadState<string>
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IPlayerFactory _playerFactory;
		private readonly IUIFactory _uiFactory;
		private readonly ISceneLoadInformer _sceneLoadInformer;

		public SceneLoadState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
			IPlayerFactory playerFactory, IUIFactory uiFactory, ISceneLoadInformer sceneLoadInformer)
		{
			_playerFactory = playerFactory;
			_uiFactory = uiFactory;
			_sceneLoadInformer = sceneLoadInformer;
			_gameStateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
		}

		public void Enter(string levelName)
		{
			_loadingCurtain.Show();
			_sceneLoader.Load(levelName, OnLoaded);
		}

		private async void OnLoaded()
		{
			await _playerFactory.CreateHero(CreateInitialPoint());
			await _uiFactory.CreateUI();

			_sceneLoadInformer.OnSceneLoaded();
			_gameStateMachine.Enter<GameLoopState>();
		}

		private GameObject CreateInitialPoint() => GameObject.FindWithTag(ConstantNames.PlayerSpawnPointTag);

		public void Exit() =>
			_loadingCurtain.Hide();
	}
}
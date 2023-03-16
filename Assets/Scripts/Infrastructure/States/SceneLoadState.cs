﻿using Infrastructure.GameLoading;
using Infrastructure.GameLoading.AssetManagement;
using Infrastructure.GameLoading.Factory;
using Infrastructure.Services;
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

		private readonly ISceneLoad _sceneLoad;

		private GameObject InitialPoint => GameObject.FindWithTag(ConstantNames.ConstantNames.PlayerSpawnPointTag);

		public SceneLoadState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
			IPlayerFactory playerFactory, IUIFactory uiFactory, ISceneLoad sceneLoad)
		{
			_playerFactory = playerFactory;
			_uiFactory = uiFactory;
			_sceneLoad = sceneLoad;
			_gameStateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
		}

		public void Enter(string levelName)
		{
			var provider = ServiceLocator.Container.GetSingle<IAssetProvider>();
			provider.CleanUp();
			_loadingCurtain.Show();
			_sceneLoader.Load(levelName, OnLoaded);
		}

		private async void OnLoaded()
		{
			await _playerFactory.InstantiateHero(InitialPoint);
			await _uiFactory.CreateUI();

			_sceneLoad.SceneLoaded += OnSceneLoaded;
			_sceneLoad.InvokeSceneLoaded();
			_loadingCurtain.Hide();
		}

		private void OnSceneLoaded()
		{
			_gameStateMachine.Enter<GameLoopState>();
			_sceneLoad.SceneLoaded -= OnSceneLoaded;
		}

		public void Exit() =>
			_loadingCurtain.Hide();
	}
}
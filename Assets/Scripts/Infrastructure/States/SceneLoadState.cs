﻿using Infrastructure.Constants;
using Infrastructure.GameLoading;
using Infrastructure.GameLoading.AssetManagement;
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
		private readonly ICameraFactory _cameraFactory;
		
		private readonly ISceneLoad _sceneLoad;
		

		public SceneLoadState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
			IPlayerFactory playerFactory, IUIFactory uiFactory, ISceneLoad sceneLoad, ICameraFactory cameraFactory)
		{
			_playerFactory = playerFactory;
			_uiFactory = uiFactory;
			_sceneLoad = sceneLoad;
			_cameraFactory = cameraFactory;
			_gameStateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
		}

		public void Enter(string levelName)
		{
			var a = ServiceLocator.Container.GetSingle<IAssetProvider>();
			a.CleanUp();
			
			_loadingCurtain.Show();
			_sceneLoader.Load(levelName, OnLoaded);
		}

		private async void OnLoaded()
		{
			await _playerFactory.CreateHero(CreateInitialPoint());
			await _uiFactory.CreateUI();

			_sceneLoad.SceneLoaded += OnSceneLoaded;
			_sceneLoad.InvokeSceneLoaded();
			
		}

		private void OnSceneLoaded()
		{
			_gameStateMachine.Enter<GameLoopState>();
			_sceneLoad.SceneLoaded -= OnSceneLoaded;
		}
		
		private GameObject CreateInitialPoint() => GameObject.FindWithTag(ConstantNames.PlayerSpawnPointTag);

		public void Exit() =>
			_loadingCurtain.Hide();
	}
}
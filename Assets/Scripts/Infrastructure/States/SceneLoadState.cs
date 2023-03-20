using Cysharp.Threading.Tasks;
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

		private IPlayerFactory _playerFactory;
		private IUIFactory _uiFactory;

		private ISceneLoad _sceneLoad;

		private GameObject InitialPoint => GameObject.FindWithTag(ConstantNames.ConstantNames.PlayerSpawnPointTag);

		public SceneLoadState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
		{
			_gameStateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			
		}

		public void Enter(string levelName)
		{
			_playerFactory = ServiceLocator.Container.GetSingle<IPlayerFactory>();
			_uiFactory = ServiceLocator.Container.GetSingle<IUIFactory>();
			_sceneLoad = ServiceLocator.Container.GetSingle<ISceneLoad>();
			
			var provider = ServiceLocator.Container.GetSingle<IAssetProvider>();
			provider.CleanUp();
			_loadingCurtain.Show();
			_sceneLoader.Load(levelName, StartLoading);
		}

		private async void StartLoading()
		{
			await Load();
		}
		
		private async UniTask  Load()
		{
			await OnLoaded();
		}
		
		private async UniTask OnLoaded()
		{
			UniTask playerFactoryTask = _playerFactory.InstantiateHero(InitialPoint);
			
			await playerFactoryTask;
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
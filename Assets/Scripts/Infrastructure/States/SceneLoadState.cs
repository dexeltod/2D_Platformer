using Infrastructure.Constants;
using Infrastructure.GameLoading;
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

		public SceneLoadState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IPlayerFactory playerFactory)
		{
			_playerFactory = playerFactory;
			_gameStateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
		}

		public void Enter(string levelName)
		{
			_loadingCurtain.Show();
			_sceneLoader.Load(levelName, OnLoaded);
		}

		private void OnLoaded()
		{
			GameObject hero = _playerFactory.CreateHero(CreateInitialPoint());
			
			
			_gameStateMachine.Enter<GameLoopState>();
		}

		private GameObject CreateInitialPoint() => GameObject.FindWithTag(ConstantNames.PlayerSpawnPointTag);

		public void Exit() => 
			_loadingCurtain.Hide();
	}
}
using UI_Scripts.Curtain;
using UnityEngine;

namespace Infrastructure
{
	public class SceneLoadState : IPayloadState<string>
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IGameFactory _gameFactory;

		public SceneLoadState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory)
		{
			_gameFactory = gameFactory;
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
			_gameFactory.CreateHero(CreateInitialPoint());
			_gameStateMachine.Enter<GameLoopState>();
		}

		private GameObject CreateInitialPoint() => GameObject.FindWithTag(NameConstants.PlayerSpawnPointTag);

		public void Exit() => 
			_loadingCurtain.Hide();
	}
}
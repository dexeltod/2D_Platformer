using System;
using Infrastructure.States;
using UI_Scripts.Curtain;

namespace Infrastructure.GameLoading
{
	public class MenuState : IState
	{
		private const string MainMenu = "Main_Menu";
		
		private readonly GameStateMachine _gameStateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;

		public MenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
		{
			_gameStateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
		}

		public void Enter()
		{
			_sceneLoader.Load(MainMenu, OnMenuLoaded);
		}

		private void OnMenuLoaded()
		{
		}

		public void Exit()
		{
			_loadingCurtain.gameObject.SetActive(true);
		}
	}
}
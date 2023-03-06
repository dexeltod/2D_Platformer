using System;
using Infrastructure.Constants;
using Infrastructure.Data;
using Infrastructure.Data.PersistentProgress;
using Infrastructure.Services.SaveLoadService;
using Infrastructure.States;

namespace Infrastructure.GameLoading
{
	public class LoadProgressState : IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly IPersistentProgressService _progressService;
		private readonly ISaveLoadService _saveLoadService;

		public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService,
			ISaveLoadService saveLoadService)
		{
			_gameStateMachine = gameStateMachine;
			_progressService = progressService;
			_saveLoadService = saveLoadService;
		}

		public void Enter()
		{
			LoadProgressOrInitNew();
		}

		public void Exit()
		{
		}

		private void OnProgressLoaded()
		{
			_gameStateMachine.Enter<MenuState>();
		}
		
		private void LoadProgressOrInitNew()
		{
			_progressService.GameProgress = _saveLoadService.LoadProgress() ?? CreateNewProgress();
		}

		private GameProgress CreateNewProgress()
		{
			GameProgress gameProgress = new GameProgress(ConstantNames.FirstLevel);
			
			LoadWeaponProgress(gameProgress, OnProgressLoaded);
			return gameProgress;
		}

		private async void LoadWeaponProgress(GameProgress gameProgress, Action progressLoaded)
		{
			await gameProgress.PlayerItemsData.SetDefaultWeapon();
			progressLoaded.Invoke();
		}
	}
}
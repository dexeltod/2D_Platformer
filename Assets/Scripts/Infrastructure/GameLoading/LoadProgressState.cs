using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Data.PersistentProgress;
using Infrastructure.Services.SaveLoadService;
using Infrastructure.States;

namespace Infrastructure.GameLoading
{
	public class LoadProgressState : IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly IPersistentProgressService _progressService;
		private readonly ISaveLoadDataService _saveLoadDataService;

		public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService,
			ISaveLoadDataService saveLoadDataService)
		{
			_gameStateMachine = gameStateMachine;
			_progressService = progressService;
			_saveLoadDataService = saveLoadDataService;
			
		}

		public async void Enter()
		{
			await LoadProgressOrInitNew(OnProgressLoaded);
		}

		public void Exit()
		{
			
		}

		private void OnProgressLoaded()
		{
			_gameStateMachine.Enter<BootstrapState>();
		}
		
		private async UniTask LoadProgressOrInitNew(Action progressLoaded)
		{
			_progressService.GameProgress = await _saveLoadDataService.LoadProgress();
			progressLoaded.Invoke();
		}
		
	}
}
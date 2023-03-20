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
		private readonly ISaveLoadService _saveLoadService;

		public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService,
			ISaveLoadService saveLoadService)
		{
			_gameStateMachine = gameStateMachine;
			_progressService = progressService;
			_saveLoadService = saveLoadService;
			
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
			_progressService.GameProgress = await _saveLoadService.LoadProgress();
			progressLoaded.Invoke();
		}
		
	}
}
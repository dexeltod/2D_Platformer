using Infrastructure.Constants;
using Infrastructure.Data;
using Infrastructure.Data.PersistentProgress;
using Infrastructure.GameLoading.Factory;
using Infrastructure.Services.SaveLoadService;
using Infrastructure.States;

namespace Infrastructure.GameLoading
{
	public class LoadProgressState : IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
		{
			_gameStateMachine = gameStateMachine;
			_progressService = progressService;
            _saveLoadService = saveLoadService;
        }

		public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<SceneLoadState, string>(ConstantNames.FirstLevel);
        }

		public void Exit()
		{
		}

		private void LoadProgressOrInitNew()
        {
            _progressService.GameProgress = _saveLoadService.LoadProgress() ?? CreateNewProgress();
        }

        private GameProgress CreateNewProgress() => 
            new GameProgress(ConstantNames.FirstLevel);
    }
}
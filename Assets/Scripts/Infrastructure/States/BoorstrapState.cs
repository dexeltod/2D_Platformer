using System;
using System.ComponentModel.Design;
using Infrastructure.Data.PersistentProgress;
using Infrastructure.GameLoading;
using Infrastructure.Services;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.States
{
	public class InitializeServicesWithProgressState : IState
	{
		private readonly GameStateMachine _gameStateMachine;

		private readonly ServiceLocator _serviceLocator;

		public InitializeServicesWithProgressState(GameStateMachine gameStateMachine, ServiceLocator serviceLocator)
		{
			_gameStateMachine = gameStateMachine;

			_serviceLocator = serviceLocator;
			
		}

		public void Enter()
		{
			RegisterServices();
			_gameStateMachine.Enter<MenuState>();
		}

		public void Exit()
		{
		}

		private void RegisterServices()
		{
			_serviceLocator.RegisterAsSingle<IInputService>(new InputService());
			_serviceLocator.RegisterAsSingle<IPlayerFactory>(
				new PlayerFactory(_serviceLocator.GetSingle<IAssetProvider>(), _serviceLocator.GetSingle<IPersistentProgressService>()));
			
			
		}
	}
}
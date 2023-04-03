using Game.Sound.Music;
using Infrastructure.Data;
using Infrastructure.Data.PersistentProgress;
using Infrastructure.Services;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Interfaces;
using Infrastructure.Services.SaveLoadService;
using Infrastructure.States;

namespace Infrastructure.GameLoading
{
	public class InitializeServicesState : IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly ServiceLocator _serviceLocator;
		private readonly SceneLoader _sceneLoader;
		private readonly SoundSetter _soundSetter;

		public InitializeServicesState(GameStateMachine gameStateMachine, ServiceLocator serviceLocator, SoundSetter soundSetter,  SceneLoader sceneLoader)
		{
			_sceneLoader = sceneLoader;
			_soundSetter = soundSetter;
			_gameStateMachine = gameStateMachine;
			_serviceLocator = serviceLocator;
			RegisterServices();
		}

		public void Exit()
		{
		}

		public void Enter()
		{
			_sceneLoader.Load(ConstantNames.ConstantNames.MenuScene, OnSceneLoaded);
		}

		private void OnSceneLoaded() =>
			_gameStateMachine.Enter<LoadProgressState>();
		
		private void RegisterServices()
		{
			_serviceLocator.RegisterAsSingle<IGameStateMachine>(_gameStateMachine);
			_serviceLocator.RegisterAsSingle<IPersistentProgressService>(new PersistentProgressService());
			_serviceLocator.RegisterAsSingle<IAssetProvider>(new AssetProvider());
			_serviceLocator.RegisterAsSingle<ISaveLoadDataService>(new SaveLoadDataService(new GameProgressFactory()));
			_serviceLocator.RegisterAsSingle<IMusicService>(new MusicService(_soundSetter, _serviceLocator.GetSingle<IAssetProvider>()));
			
			SceneLoadInformer sceneLoadInformer = new SceneLoadInformer();
	        
			_serviceLocator.RegisterAsSingle<ISceneLoadInformer>(sceneLoadInformer);
			_serviceLocator.RegisterAsSingle<ISceneLoad>(sceneLoadInformer);
			_serviceLocator.RegisterAsSingle<IUIFactory>(new UIFactory());
			
			CameraFactory cameraFactory = new CameraFactory();
			_serviceLocator.RegisterAsSingle<ICameraFactory>(cameraFactory);
			_serviceLocator.RegisterAsSingle<ICamera>(cameraFactory);
			_serviceLocator.RegisterAsSingle<ISceneConfigGetter>(new SceneConfigGetter());
		}
	}
}
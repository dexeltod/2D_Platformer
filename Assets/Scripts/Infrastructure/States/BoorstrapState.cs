using Infrastructure.Constants;
using Infrastructure.Data.PersistentProgress;
using Infrastructure.GameLoading;
using Infrastructure.GameLoading.AssetManagement;
using Infrastructure.GameLoading.Factory;
using Infrastructure.Services;
using Infrastructure.Services.SaveLoadService;

namespace Infrastructure.States
{
	public class BootstrapState : IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly ServiceLocator _serviceLocator;

		public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ServiceLocator serviceLocator)
		{
			_gameStateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_serviceLocator = serviceLocator;
			RegisterServices();
		}

		public void Enter()
		{
			_sceneLoader.Load(ConstantNames.InitialScene, OnSceneLoaded);
		}

		public void Exit()
		{
		}

        private void OnSceneLoaded() =>
            _gameStateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
			_serviceLocator.RegisterAsSingle<IGameStateMachine>(_gameStateMachine);
			_serviceLocator.RegisterAsSingle<IPersistentProgressService>(new PersistentProgressService());
			_serviceLocator.RegisterAsSingle<IAssetProvider>(new AssetProvider());
			
	        SceneLoadInformer sceneLoadInformer = new SceneLoadInformer();
	        CameraFactory cameraFactory = new CameraFactory();

	        _serviceLocator.RegisterAsSingle<ISaveLoadService>(new SaveLoadService());
			_serviceLocator.RegisterAsSingle<IInputService>(new InputService());
			
			_serviceLocator.RegisterAsSingle<ISceneLoadInformer>(sceneLoadInformer);
			_serviceLocator.RegisterAsSingle<ISceneLoad>(sceneLoadInformer);
			
			_serviceLocator.RegisterAsSingle<IUIFactory>(new UIFactory());
			
			_serviceLocator.RegisterAsSingle<ICameraFactory>(cameraFactory);
			_serviceLocator.RegisterAsSingle<ICamera>(cameraFactory);

			_serviceLocator.RegisterAsSingle<IPlayerFactory>(
				new PlayerFactory(_serviceLocator.GetSingle<IAssetProvider>()));
		}
    }
}
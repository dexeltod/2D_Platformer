using Infrastructure.AssetProvide;
using Infrastructure.Services;

namespace Infrastructure
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
			_sceneLoader.Load(NameConstants.InitialScene, EnterLoadLevel);
		}

		private void RegisterServices()
		{
			_serviceLocator.RegisterAsSingle<IGameStateMachine>(_gameStateMachine);
			_serviceLocator.RegisterAsSingle<IAssetProvider>(new AssetProvider());
			_serviceLocator.RegisterAsSingle<IGameFactory>(new GameFactory(_serviceLocator.Single<IAssetProvider>()));
			_serviceLocator.RegisterAsSingle<IInputService>(new InputService());
		}

		private void EnterLoadLevel() =>
			_gameStateMachine.Enter<SceneLoadState, string>(NameConstants.FirstLevel);

		public void Exit()
		{
		}
	}
}
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
			_sceneLoader.Load(ConstantNames.InitialScene, EnterLoadLevel);
		}

		private void RegisterServices()
		{
			_serviceLocator.RegisterAsSingle<IGameStateMachine>(_gameStateMachine);
			_serviceLocator.RegisterAsSingle<IAssetProvider>(new AssetProvider());
			_serviceLocator.RegisterAsSingle<IInputService>(new InputService());
			_serviceLocator.RegisterAsSingle<IPlayerFactory>(new PlayerFactory(_serviceLocator.Single<IAssetProvider>()));
		}

		private void EnterLoadLevel() =>
			_gameStateMachine.Enter<SceneLoadState, string>(ConstantNames.FirstLevel);

		public void Exit()
		{
		}
	}
}
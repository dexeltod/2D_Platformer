using Infrastructure.Services;
using Infrastructure.States;
using UI_Scripts.Curtain;

namespace Infrastructure.GameLoading
{
	public class MenuState : IState
	{
		private const string MainMenu = "Main_Menu";
		
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly ISoundService _soundService;

		public MenuState(SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
		{
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_soundService = ServiceLocator.Container.GetSingle<ISoundService>();
		}

		public void Enter()
		{
			_soundService.Set(ConstantNames.ConstantNames.MusicNames.MenuMusic);
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
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Infrastructure.States;
using View.UI_Scripts.Curtain;

namespace Infrastructure.GameLoading
{
	public class MenuState : IState
	{
		private const string MainMenu = "Main_Menu";
		
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IMusicService _musicService;

		public MenuState(SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
		{
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_musicService = ServiceLocator.Container.GetSingle<IMusicService>();
		}

		public void Enter()
		{
			_musicService.Set(ConstantNames.ConstantNames.MusicNames.MenuMusic);
			_sceneLoader.Load(MainMenu);
		}
		public void Exit()
		{
			_loadingCurtain.gameObject.SetActive(true);
		}
	}
}
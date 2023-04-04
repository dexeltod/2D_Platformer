using Cysharp.Threading.Tasks;
using Game.SceneConfigs;
using Infrastructure.Services.Interfaces;
using Infrastructure.States;
using UnityEngine.Device;
using UnityEngine.UIElements;
using ViewModel.StartMenu.UIBuilder;

namespace ViewModel.StartMenu.MenuWindows
{
	public class MainMenu : MenuElement
	{
		private const string MainMenuElement = "MainMenu";
		private const string LevelsMenuElement = "Levels";
		private const string SettingsMenuElement = "Settings";
		private const string FirstLevelName = "Level_1";

		private readonly UIElementGetterFacade _uiElementGetter;
		private readonly VisualElementViewModel _visualElementSwitcher;
		private readonly ISceneConfigGetter _sceneConfigGetter;
		private readonly IGameStateMachine _gameStateMachine;
		private VisualElement _menuVisualElement;
		private VisualElement _levelsVisualElement;
		private VisualElement _settingsVisualElement;

		private Button _playButton;
		private Button _levelsButton;
		private Button _settingsButton;
		private Button _exitButton;

		public MainMenu(VisualElement thisElement, UIElementGetterFacade uiElementGetter,
			VisualElementViewModel visualElementSwitcher, ISceneConfigGetter sceneConfigGetter,
			IGameStateMachine gameStateMachine) : base(thisElement, visualElementSwitcher, uiElementGetter)
		{
			_uiElementGetter = uiElementGetter;
			_visualElementSwitcher = visualElementSwitcher;
			_sceneConfigGetter = sceneConfigGetter;
			_gameStateMachine = gameStateMachine;
			Initialize();
		}

		private void Initialize()
		{
			GetElementsToSwitch();
			GetButtons();
			SubscribeOnButtons();
		}

		~MainMenu()
		{
			UnsubscribeFromButtons();
		}

		private void GetElementsToSwitch()
		{
			_levelsVisualElement = _uiElementGetter.GetFirst<VisualElement>(LevelsMenuElement);
			_settingsVisualElement = _uiElementGetter.GetFirst<VisualElement>(SettingsMenuElement);
		}

		private void GetButtons()
		{
			_playButton = _uiElementGetter.GetFirst<Button>(UiButtonNames.Play);
			_levelsButton = _uiElementGetter.GetFirst<Button>(UiButtonNames.Levels);
			_settingsButton = _uiElementGetter.GetFirst<Button>(UiButtonNames.Settings);
			_exitButton = _uiElementGetter.GetFirst<Button>(UiButtonNames.Exit);
		}

		private void SubscribeOnButtons()
		{
			_playButton.clicked += OnPlay;
			_levelsButton.clicked += OnOpenLevelsMenu;
			_settingsButton.clicked += OpenSettingsMenu;
			_exitButton.clicked += OnExitGame;
		}

		private void UnsubscribeFromButtons()
		{
			_playButton.clicked -= OnPlay;
			_levelsButton.clicked -= OnOpenLevelsMenu;
			_settingsButton.clicked -= OpenSettingsMenu;
			_exitButton.clicked -= OnExitGame;
		}

		private void OnExitGame() =>
			Application.Quit();

		private void OnOpenLevelsMenu() =>
			_visualElementSwitcher.Enter(ThisElement, _levelsVisualElement);

		private void OpenSettingsMenu() =>
			_visualElementSwitcher.Enter(ThisElement, _settingsVisualElement);

		private async void OnPlay()
		{
			_visualElementSwitcher.Disable(ThisElement);
			SceneConfig sceneConfig = await _sceneConfigGetter.GetSceneConfig(FirstLevelName);
			
			_gameStateMachine.Enter<SceneLoadState, string, bool>(sceneConfig.SceneName,
				sceneConfig.MusicName, sceneConfig.IsStopMusicBetweenScenes);
		}
	}
}
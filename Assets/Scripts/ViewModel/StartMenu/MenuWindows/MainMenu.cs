using UnityEngine.Device;
using UnityEngine.UIElements;
using View.StartMenu.UIBuilder;

namespace ViewModel.MainMenu.Buttons
{
	public class MainMenu : MenuElement
	{
		private const string MainMenuElement = "MainMenu";
		private const string LevelsMenuElement = "Levels";
		private const string SettingsMenuElement = "Settings";

		private readonly UIElementGetterFacade _uiElementGetter;
		private readonly VisualElementSwitcher _visualElementSwitcher;
		private VisualElement _menuVisualElement;
		private VisualElement _levelsVisualElement;
		private VisualElement _settingsVisualElement;

		private Button _playButton;
		private Button _levelsButton;
		private Button _settingsButton;
		private Button _exitButton;

		public MainMenu(VisualElement thisElement, UIElementGetterFacade uiElementGetter,
			VisualElementSwitcher visualElementSwitcher) : base(thisElement, visualElementSwitcher, uiElementGetter)
		{
			_uiElementGetter = uiElementGetter;
			_visualElementSwitcher = visualElementSwitcher;
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
			_levelsVisualElement = _uiElementGetter.GetUIElementQ<VisualElement>(LevelsMenuElement);
			_settingsVisualElement = _uiElementGetter.GetUIElementQ<VisualElement>(SettingsMenuElement);
		}

		private void GetButtons()
		{
			_playButton = _uiElementGetter.GetUIElementQ<Button>(UiButtonNames.Play);
			_levelsButton = _uiElementGetter.GetUIElementQ<Button>(UiButtonNames.Levels);
			_settingsButton = _uiElementGetter.GetUIElementQ<Button>(UiButtonNames.Settings);
			_exitButton = _uiElementGetter.GetUIElementQ<Button>(UiButtonNames.Exit);
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

		private void OnPlay()
		{
		}
	}
}
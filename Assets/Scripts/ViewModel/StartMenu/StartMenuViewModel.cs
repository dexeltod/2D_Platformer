using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;
using View.StartMenu.UIBuilder;

namespace ViewModel.MainMenu.Buttons
{
	[RequireComponent(typeof(UIElementGetterFacade))]
	public class StartMenuViewModel : MonoBehaviour
	{
		[SerializeField] private AudioMixer _audioMixer;
		[SerializeField] private AudioSource _buttonSound;
		
		private const string MainMenu = "MainMenu";
		private const string Settings = "Settings";

		private VisualElement _startMenu;
		private UIElementGetterFacade _uiElementGetter;

		private VisualElementSwitcher _visualElementSwitcher;

		private MainMenu _mainMenu;
		private SettingsMenu _settingsMenu;
		private LevelsMenu _levelsMenu;

		private List<Button> _allButtons;

		private void Awake()
		{
			_uiElementGetter = GetComponent<UIElementGetterFacade>();
			_visualElementSwitcher = new VisualElementSwitcher();

			CreateMenuWindows();
			_allButtons = _uiElementGetter.GetAllElementsByType<Button>();
			SubscribeOnButtons();
		}

		private void OnDestroy() => 
			UnsubscribeFromButtons();

		private void CreateMenuWindows()
		{
			_mainMenu = new MainMenu(_uiElementGetter.GetUIElementQ<VisualElement>(MainMenu), _uiElementGetter, _visualElementSwitcher);
			_settingsMenu = new SettingsMenu(_uiElementGetter.GetUIElementQ<VisualElement>(Settings),_uiElementGetter, _visualElementSwitcher, _audioMixer);
			_levelsMenu = new LevelsMenu(_uiElementGetter, _visualElementSwitcher);
		}

		private void SubscribeOnButtons()
		{
			foreach (var button in _allButtons) 
				button.clicked += PlayButtonSound;
		}

		private void UnsubscribeFromButtons()
		{
			foreach (var button in _allButtons) 
				button.clicked -= PlayButtonSound;
		}

		private void PlayButtonSound() => 
			_buttonSound.Play();
	}
}
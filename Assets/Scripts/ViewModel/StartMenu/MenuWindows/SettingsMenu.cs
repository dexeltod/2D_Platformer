using System;
using System.Collections.Generic;
using AudioSettings;
using UnityEngine.Audio;
using UnityEngine.UIElements;
using View.StartMenu.SettingsWindow.Sliders;
using View.StartMenu.UIBuilder;

namespace ViewModel.MainMenu.Buttons
{
	public class SettingsMenu : MenuElement
	{
		private readonly AudioMixer _audioMixer;
		public event Action<Dictionary<string, float>> SettingsChanged;
		private VisualElement _menuElement;
		private SettingsViewModel _settingsViewModel;
		private SaveLoadAudioMixerSettingsModel _saveLoadAudioMixerSettings;

		private Button _saveButton;
		private Button _menuButton;
		private List<Slider> _slidersViewModel;

		public SettingsMenu(VisualElement thisElement, UIElementGetterFacade uiElementGetterFacade,
			VisualElementSwitcher visualElementSwitcher, AudioMixer audioMixer) : base(thisElement,
			visualElementSwitcher,
			uiElementGetterFacade)
		{
			_audioMixer = audioMixer;
			Initialize();
		}

		~SettingsMenu() => 
			UnsubscribeFromButtons();

		private void Initialize()
		{
			new SettingsSliderView(ElementGetter, this);
			_saveLoadAudioMixerSettings = new();
			_settingsViewModel = new(_audioMixer, ElementGetter, _saveLoadAudioMixerSettings);
			GetVisualElements();
			SubscribeOnButtons();
			OnLoadSettings();
		}

		private void GetVisualElements()
		{
			_menuElement = ElementGetter.GetUIElementQ<VisualElement>(MenuVisualElementNames.Menu);
			_saveButton = ElementGetter.GetUIElementQ<Button>(UiButtonNames.Save);
			_menuButton = ElementGetter.GetUIElementQ<Button>(UiButtonNames.Menu);
		}

		private void SubscribeOnButtons()
		{
			_saveButton.clicked += OnButtonSaveClicked;
			_menuButton.clicked += OnMenuButtonClocked;
		}

		private void UnsubscribeFromButtons()
		{
			_saveButton.clicked -= OnButtonSaveClicked;
			_menuButton.clicked -= OnMenuButtonClocked;
		}

		private void OnMenuButtonClocked() =>
			VisualElementSwitcher.Enter(ThisElement, _menuElement);

		private void OnButtonSaveClicked()
		{
			var sliders = _settingsViewModel.GetValuesInDictionary();

			foreach (var slider in sliders)
				_saveLoadAudioMixerSettings.Save(slider.Key, slider.Value);
		}

		private void OnLoadSettings()
		{
			Dictionary<string, float> settings = _saveLoadAudioMixerSettings.Load();
			SettingsChanged?.Invoke(settings);
		}
	}
}
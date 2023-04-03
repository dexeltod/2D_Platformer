using System;
using System.Collections.Generic;
using Infrastructure.Services.Interfaces;
using Model.AudioSettings;
using UnityEngine.Audio;
using UnityEngine.UIElements;
using View.StartMenu.SettingsWindow.Sliders;
using ViewModel.StartMenu.UIBuilder;

namespace ViewModel.StartMenu.MenuWindows
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
			VisualElementViewModel visualElementSwitcher, AudioMixer audioMixer) : base(thisElement,
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
			_menuElement = ElementGetter.GetFirst<VisualElement>(MenuVisualElementNames.Menu);
			_saveButton = ElementGetter.GetFirst<Button>(UiButtonNames.Save);
			_menuButton = ElementGetter.GetFirst<Button>(UiButtonNames.Menu);
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
			VisualElementController.Enter(ThisElement, _menuElement);

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
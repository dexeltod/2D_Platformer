using System;
using System.Collections.Generic;
using AudioSettings;
using UnityEngine;
using UnityEngine.UIElements;
using View.StartMenu.UIBuilder;
using View.StartMenu.UIBuilder.SliderViewModel;

namespace ViewModel.MainMenu.Buttons
{
	[RequireComponent(typeof(UIElementGetterFacadeView))]
	public class SaveSettingsButtonViewModel : MonoBehaviour
	{
		[SerializeField] private SettingsSliderViewModel _slidersViewModel;
		private const string SaveButton = "SaveButton";

		public SaveLoadAudioMixerSettings SaveLoadAudioMixerSettings { get; private set; }
		public event Action<Dictionary<string, float>> SettingsChanged;

		private UIElementGetterFacadeView _uiElementGetterFacadeView;

		private Button _saveButton;

		private void Awake() => 
			_uiElementGetterFacadeView = GetComponent<UIElementGetterFacadeView>();

		private void OnEnable()
		{
			Initialize();
			_saveButton = _uiElementGetterFacadeView.GetUIElementQ<Button>(SaveButton);
			_saveButton.clicked += OnSaveSaveButtonClick;
			OnLoadSettings();
		}

		private void OnDisable()
		{
			_saveButton.clicked -= OnSaveSaveButtonClick;
		}

		public void Initialize()
		{
			if (SaveLoadAudioMixerSettings != null)
				return;
			
			SaveLoadAudioMixerSettings = new();
		}

		private void OnSaveSaveButtonClick()
		{
			var sliders = _slidersViewModel.GetValues();

			foreach (var slider in sliders)
				SaveLoadAudioMixerSettings.Save(slider.Key, slider.Value);
		}

		private void OnLoadSettings()
		{
			Dictionary<string, float> settings = SaveLoadAudioMixerSettings.LoadSettings();

			SettingsChanged?.Invoke(settings);
		}
	}
}
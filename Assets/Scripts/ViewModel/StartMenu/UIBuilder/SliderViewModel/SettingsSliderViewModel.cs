using System.Collections.Generic;
using System.Linq;
using AudioSettings;
using UnityEngine;
using ViewModel.MainMenu.Buttons;

namespace View.StartMenu.UIBuilder.SliderViewModel
{
	[RequireComponent(typeof(UIElementGetterFacadeView))]
	[RequireComponent(typeof(SaveSettingsButtonViewModel))]
	public class SettingsSliderViewModel : MonoBehaviour
	{
		private SaveSettingsButtonViewModel _saveSettingsButtonViewModel;

		private string[] _sliderNames;

		private SaveLoadAudioMixerSettings _saveLoadAudioMixerSettings;
		private SliderUIProvider _sliderUIProvider;
		private Dictionary<string, float> _sliders = new();

		private void Awake()
		{
			_saveSettingsButtonViewModel = GetComponent<SaveSettingsButtonViewModel>();
			_saveSettingsButtonViewModel.Initialize();
			_saveLoadAudioMixerSettings = _saveSettingsButtonViewModel.SaveLoadAudioMixerSettings;
			UIElementGetterFacadeView document = GetComponent<UIElementGetterFacadeView>();
			_sliderUIProvider = new SliderUIProvider(document);

			InitializeSliderNames();
		}

		private void OnEnable()
		{
			_saveSettingsButtonViewModel.Initialize();
			LoadLastSettingsOrInitNew(_saveLoadAudioMixerSettings);
		}

		public Dictionary<string, float> GetValues()
		{
			Dictionary<string, float> newSliders = new();

			foreach (var slider in _sliderNames)
			{
				float value = _sliderUIProvider.GetSlider(slider).value;
				newSliders.Add(slider, value);
			}

			return newSliders.ToDictionary(e => e.Key, e => e.Value);
		}

		private void InitializeSliderNames()
		{
			_sliderNames = new[]
			{
				UIButtonsNames.Master,
				UIButtonsNames.Music,
			};
		}

		private void LoadLastSettingsOrInitNew(SaveLoadAudioMixerSettings settings)
		{
			_sliders.Clear();

			_sliders = settings.LoadSettings();

			if (_sliders.Count <= 0)
				SetDefaultValues();
			
			foreach (var value in _sliders) 
				_sliderUIProvider.SetSliderValue(value.Key, value.Value);
		}

		private void SetDefaultValues()
		{
			foreach (var slider in _sliderNames)
			{
				float value = _sliderUIProvider.GetSlider(slider).value;
				_sliders.Add(slider, value);
			}
		}
	}
}
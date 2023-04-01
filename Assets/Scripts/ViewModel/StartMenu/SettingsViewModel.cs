using System.Collections.Generic;
using System.Linq;
using AudioSettings;
using UnityEngine.Audio;
using View.StartMenu.SettingsWindow;
using View.StartMenu.UIBuilder;
using View.StartMenu.UIBuilder.SliderViewModel;

namespace ViewModel.MainMenu.Buttons
{
	public class SettingsViewModel
	{
		private readonly AudioMixer _audioMixer;
		private readonly UIElementGetterFacade _elementGetter;

		private readonly List<string> _sliderNames = new();

		private readonly SaveLoadAudioMixerSettingsModel _saveLoadAudioMixerSettings;
		private List<AudioMixerGroup> _audioMixerGroup;
		private Dictionary<string, float> _sliderValues = new();

		private SliderUIProvider _sliderUIProvider;
		private SoundVolumeSetter _soundSetter;

		public SettingsViewModel(AudioMixer audioMixer, UIElementGetterFacade elementGetter,
			SaveLoadAudioMixerSettingsModel saveLoadAudioMixerSettings)
		{
			_audioMixer = audioMixer;
			_elementGetter = elementGetter;
			_saveLoadAudioMixerSettings = saveLoadAudioMixerSettings;
			Initialize();
		}

		private void Initialize()
		{
			_audioMixerGroup = _audioMixer.FindMatchingGroups(string.Empty).ToList();
			_sliderUIProvider = new SliderUIProvider(_elementGetter);

			_soundSetter = new SoundVolumeSetter(_sliderUIProvider, _audioMixer);

			SetSliderNames();
			_soundSetter.RegisterOnSliderValueChanges(_sliderNames);
			LoadLastSettingsOrInitNew(_saveLoadAudioMixerSettings);
		}

		public Dictionary<string, float> GetValuesInDictionary()
		{
			Dictionary<string, float> newSliders = new();

			foreach (var slider in _sliderNames)
			{
				float value = _sliderUIProvider.GetSlider(slider).value;
				newSliders.Add(slider, value);
			}

			return newSliders;
		}

		private void SetSliderNames()
		{
			if (_sliderNames.Count > 0)
				_sliderNames.Clear();

			foreach (var group in _audioMixerGroup)
				_sliderNames.Add(group.name);
		}

		private void LoadLastSettingsOrInitNew(SaveLoadAudioMixerSettingsModel settings)
		{
			_sliderValues.Clear();
			_sliderValues = settings.Load();

			if (_sliderValues.Count <= 0)
				SetDefaultValues();

			foreach (var value in _sliderValues)
				_sliderUIProvider.SetSliderValue(value.Key, value.Value);

			foreach (var slider in _sliderValues)
				_soundSetter.SetValue(slider.Key, slider.Value);
		}

		private void SetDefaultValues()
		{
			foreach (var slider in _sliderNames)
			{
				float value = _sliderUIProvider.GetSlider(slider).value;
				_sliderValues.Add(slider, value);
			}
		}
	}
}
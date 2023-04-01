using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UIElements;
using View.StartMenu.UIBuilder.SliderViewModel;

namespace View.StartMenu.SettingsWindow
{
	public class SoundVolumeSetter
	{
		private readonly SliderUIProvider _sliderUIProvider;
		private readonly AudioMixer _audioMixer;
		private readonly List<Slider> _sliders = new();

		public SoundVolumeSetter(SliderUIProvider sliderUIProvider, AudioMixer audioMixer)
		{
			_sliderUIProvider = sliderUIProvider;
			_audioMixer = audioMixer;
		}

		~SoundVolumeSetter()
		{
			foreach (var slider in _sliders)
				slider.UnregisterValueChangedCallback(OnSliderChanged);
		}

		public void SetValue(string soundName, float soundValue) => 
			_audioMixer.SetFloat(soundName, soundValue);

		public void RegisterOnSliderValueChanges(List<string> sliderNames)
		{
			foreach (var slider in sliderNames)
				_sliders.Add(_sliderUIProvider.GetSlider(slider));

			foreach (var slider in _sliders)
				slider.RegisterValueChangedCallback(OnSliderChanged);
		}

		private void OnSliderChanged(ChangeEvent<float> changedValue)
		{
			Slider changedTarget = (Slider)changedValue.target;
			_audioMixer.SetFloat(changedTarget.name, changedValue.newValue);
		}
	}
}
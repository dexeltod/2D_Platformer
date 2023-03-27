using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace AudioSettings
{
	internal enum SoundType
	{
		Master,
		Music,
	};

	public class AudioMixerSetter : MonoBehaviour
	{
		private const float Multiplier = 20f;

		[SerializeField] private SoundType _namesType;
		[SerializeField] private AudioMixer _audioMixer;
		[SerializeField] private Slider _slider;
		
		private SaveLoadAudioMixerSettings _saveLoadAudioMixerSettings;

		private void Awake()
		{
			_saveLoadAudioMixerSettings = new SaveLoadAudioMixerSettings();
			_slider.value = _saveLoadAudioMixerSettings.LoadFloat();
		}

		private void OnEnable()
		{
			_slider.onValueChanged.AddListener(OnChangeVolumeValue);
		}

		private void OnDisable()
		{
			_saveLoadAudioMixerSettings.Save(GetSoundTypeName(), _slider.value);
			_slider.onValueChanged.RemoveListener(OnChangeVolumeValue);
		}

		private void OnChangeVolumeValue(float newValue)
		{
			float countedVolume = Mathf.Log10(newValue) * Multiplier;
			_audioMixer.SetFloat(GetSoundTypeName(), countedVolume);
			
		}

		private string GetSoundTypeName() =>
			Enum.GetName(typeof(SoundType), _namesType);
	}
}
using System;
using System.Collections.Generic;
using Infrastructure.GameLoading;
using Infrastructure.Services.SaveLoadService;
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
			_saveLoadAudioMixerSettings = new SaveLoadAudioMixerSettings(GetSoundTypeName());
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

	public class SaveLoadAudioMixerSettings
	{
		private const string FileName = "AudioSettings";
		private readonly ISaveLoadDataService _saveLoadService;
		private readonly AudioSettingsData _audioSettingsData;
		private readonly string _settingType;
		
		public SaveLoadAudioMixerSettings(string settingType)
		{
			_settingType = settingType;
			_audioSettingsData = new();
			_saveLoadService = ServiceLocator.Container.GetSingle<ISaveLoadDataService>();
			
		}

		public void Save(string settingType, float value)
		{
			_audioSettingsData.Save(settingType, value);
			_saveLoadService.SaveToJson(FileName,_audioSettingsData);
		}

		public float LoadFloat()
		{
			string json =_saveLoadService.LoadFromJson(FileName);


			return 0.5f;
		}
	}

	[Serializable]
	public class AudioSettingsData
	{
		public Dictionary<string, float> Settings = new();

		public void Save(string settingName, float value)
		{
			if (Settings.ContainsKey(settingName)) 
				Settings[settingName] = value;
			else
				Settings.Add(settingName, value);
		}

		public float GetSettingValue(string name) => 
			Settings[name];
	}
}
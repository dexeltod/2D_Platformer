using System.Collections.Generic;
using Infrastructure.GameLoading;
using Infrastructure.Services.SaveLoadService;

namespace Model.AudioSettings
{
	public class SaveLoadAudioMixerSettingsModel
	{
		private const string FileName = "AudioSettings";
		private readonly ISaveLoadDataService _saveLoadService;
		private readonly AudioSettingsData _audioSettingsData;
		private readonly string _settingType;

		public SaveLoadAudioMixerSettingsModel()
		{
			_saveLoadService = ServiceLocator.Container.GetSingle<ISaveLoadDataService>();
			_audioSettingsData = new();
		}

		public void Save(string settingType, float value)
		{
			_audioSettingsData.Save(settingType, value);
			_saveLoadService.SaveToJson(FileName, _audioSettingsData);
		}

		public Dictionary<string, float> Load()
		{
			AudioSettingsData settingsData = _saveLoadService.LoadFromJson<AudioSettingsData>(FileName) ?? new AudioSettingsData();

			Dictionary<string, float> settings = new();

			for (int i = 0; i < settingsData.Names.Count; i++)
				settings.Add(settingsData.Names[i], settingsData.Values[i]);

			return settings;
		}
	}
}
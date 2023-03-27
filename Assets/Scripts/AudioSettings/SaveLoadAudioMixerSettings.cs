using Infrastructure.GameLoading;
using Infrastructure.Services.SaveLoadService;

namespace AudioSettings
{
	public class SaveLoadAudioMixerSettings
	{
		private const string FileName = "AudioSettings";
		private readonly ISaveLoadDataService _saveLoadService;
		private readonly AudioSettingsData _audioSettingsData;
		private readonly string _settingType;
		
		public SaveLoadAudioMixerSettings()
		{
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
}
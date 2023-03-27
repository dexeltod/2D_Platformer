using System;
using System.Collections.Generic;

namespace AudioSettings
{
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
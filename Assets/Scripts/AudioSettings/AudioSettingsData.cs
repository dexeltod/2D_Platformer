using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

namespace AudioSettings
{
	[Serializable]
	public class AudioSettingsData
	{
		public List<string> Names = new();
		public List<float> Values = new();

		public void Save(string settingName, float value)
		{
			if (Names.Contains(settingName) == false)
			{
				Names.Add(settingName);
				Values.Add(value);
			}

			for (int i = 0; i < Names.Count; i++)
				if (Names[i] == settingName)
					Values[i] = value;
		}
	}
}
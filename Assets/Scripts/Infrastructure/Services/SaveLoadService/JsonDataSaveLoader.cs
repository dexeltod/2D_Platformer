using System.IO;
using UnityEngine;

namespace Infrastructure.Services.SaveLoadService
{
	public class JsonDataSaveLoader
	{
		private readonly string _fileFormat;
		private string _directoryPath => Application.persistentDataPath + "/Data/";

		public JsonDataSaveLoader()
		{
			_fileFormat = ".json";
			Directory.CreateDirectory(_directoryPath);
		}
		
		public void Save(string fileName, object data)
		{
			string file = JsonUtility.ToJson(data);
			string path = _directoryPath + fileName + _fileFormat;

			using (var writer = new StreamWriter(path))
			{
				writer.WriteLine(file);
			}
		}

		public string Load(string file)
		{
			if (File.Exists(_directoryPath + file + _fileFormat) == false) 
				File.Create(_directoryPath + file + _fileFormat);

			string json = "";
			
			using (var reader = new StreamReader(_directoryPath + file + _fileFormat))
			{
				string line;
				
				while ((line = reader.ReadLine()) != null)
				{
					json += line;
				}
			}

			return JsonUtility.FromJson<string>(json);
		}
	}
}
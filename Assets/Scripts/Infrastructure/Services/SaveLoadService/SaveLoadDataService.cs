using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Infrastructure.Data;
using Infrastructure.Data.PersistentProgress;
using Infrastructure.Data.Serializable;
using Infrastructure.GameLoading;
using UnityEngine;
using FileStream = System.IO.FileStream;

namespace Infrastructure.Services.SaveLoadService
{
	public class SaveLoadDataService : ISaveLoadDataService
	{
		private const string SavesDirectory = "/Saves/";

		private readonly BinaryDataSaveLoader _binaryDataSaveLoader;
		private readonly JsonDataSaveLoader _jsonDataLoader;
		private GameProgress _gameProgress;

		private string _lastTime;
		private string _saveFilePath;

		public SaveLoadDataService(GameProgressFactory gameProgressFactory)
		{
			string saveDirectoryPath = Application.persistentDataPath + SavesDirectory;
			Directory.CreateDirectory(saveDirectoryPath);
			
			_jsonDataLoader = new JsonDataSaveLoader();
			_binaryDataSaveLoader = new BinaryDataSaveLoader(gameProgressFactory);
			
			_gameProgress = ServiceLocator.Container.GetSingle<IPersistentProgressService>().GameProgress;
		}

		public void SaveToJson(string fileName, object data) => 
			_jsonDataLoader.Save(fileName, data);

		public string LoadFromJson(string fileName) => 
			_jsonDataLoader.Load(fileName);

		public void SaveProgress() =>
			_binaryDataSaveLoader.Save(_gameProgress);

		public async void SetStartProgress() => 
			_gameProgress = await _binaryDataSaveLoader.CreateNewProgressByBinary();

		public async UniTask<GameProgress> LoadProgress() =>
			_gameProgress = await _binaryDataSaveLoader.LoadProgress();
	}

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
			if (File.Exists(_directoryPath + file) == false) 
				File.Create(_directoryPath + file);

			string json = "";
			
			using (var reader = new StreamReader(_directoryPath + file))
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

	public class BinaryDataSaveLoader
	{
		private const string SaveFileFormat = ".data";
		private const string SavesDirectory = "/Saves/";
		private const string SaveName = "save_";

		private string _lastTime;
		private string _saveFilePath;
		private string _saveDirectoryPath => Application.persistentDataPath + SavesDirectory;
		private readonly GameProgressFactory _gameProgressFactory;

		public BinaryDataSaveLoader(GameProgressFactory gameProgressFactory)
		{
			_gameProgressFactory = gameProgressFactory;
		}

		public void Save(object data)
		{
			using (FileStream saveFile = File.Create(_saveFilePath))
			{
				new BinaryFormatter().Serialize(saveFile, data);
			}
		}

		public async UniTask<GameProgress> LoadProgress()
		{
			string[] files = Directory.GetFiles(_saveDirectoryPath);

			if (files.Length <= 0)
				await CreateNewProgressByBinary();

			files = Directory.GetFiles(_saveDirectoryPath);

			string lastSaveFilePath = files.Last();

			using FileStream file = File.Open(lastSaveFilePath, FileMode.Open);
			{
				object loadedData = new BinaryFormatter().Deserialize(file);
				GameProgress gameProgress = (GameProgress)loadedData;
				return gameProgress;
			}
		}

		public async Task<GameProgress> CreateNewProgressByBinary()
		{
			SetUniqueSaveFilePath();
			GameProgress gameProgress = await _gameProgressFactory.CreateProgress();

			using (FileStream saveFile = File.Create(_saveFilePath))
			{
				new BinaryFormatter().Serialize(saveFile, gameProgress);
			}

			return gameProgress;
		}

		private void SetUniqueSaveFilePath()
		{
			_lastTime = DateTime.UtcNow.ToString("ss.mm.hh.dd.MM.yyyy");
			_saveFilePath = _saveDirectoryPath + SaveName + _lastTime + SaveFileFormat;
		}
	}
}
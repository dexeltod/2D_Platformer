﻿using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Cysharp.Threading.Tasks;
using Infrastructure.Data;
using Infrastructure.Data.PersistentProgress;
using Infrastructure.Data.Serializable;
using Infrastructure.GameLoading;
using UnityEngine;

namespace Infrastructure.Services.SaveLoadService
{
	public class SaveLoadService : ISaveLoadService
	{
		private const string SaveFileFormat = ".data";
		private const string SavesDirectory = "/Saves/";
		private const string SaveName = "save_";

		private readonly string _saveDirectoryPath;
		private readonly GameProgressFactory _gameProgressFactory;

		private string _lastTime;
		private string _saveFilePath;
		
		private GameProgress _gameProgress;

		public SaveLoadService()
		{
			_gameProgressFactory = new GameProgressFactory();
			_saveDirectoryPath = Application.persistentDataPath + SavesDirectory;
			Directory.CreateDirectory(_saveDirectoryPath);

			_gameProgress = ServiceLocator.Container.GetSingle<IPersistentProgressService>().GameProgress;
		}

		public void SaveProgress()
		{
			using (FileStream file = File.Create(_saveFilePath))
			{
				new BinaryFormatter().Serialize(file, _gameProgress);
			}
		}

		public async void SetStartProgress()
		{
			await CreateNewProgress();
		}
		
		public async UniTask<GameProgress> LoadProgress()
		{
			string[] files = Directory.GetFiles(_saveDirectoryPath);
			
			if (files.Length <= 0)
				await CreateNewProgress();

			files = Directory.GetFiles(_saveDirectoryPath);

			string lastSaveFilePath = files.Last();

			using FileStream file = File.Open(lastSaveFilePath, FileMode.Open);
			{
				object loadedData = new BinaryFormatter().Deserialize(file);
				GameProgress gameProgress = (GameProgress)loadedData;
				return gameProgress;
			}
		}

		private async UniTask CreateNewProgress()
		{
			SetSaveFilePath();
			GameProgress gameProgress = await _gameProgressFactory.CreateProgress();

			using (FileStream saveFile = File.Create(_saveFilePath))
			{
				new BinaryFormatter().Serialize(saveFile, gameProgress);
			}
			
			_gameProgress = gameProgress;
		}

		private void SetSaveFilePath()
		{
			_lastTime = DateTime.UtcNow.ToString("ss.mm.hh.dd.MM.yyyy");
			_saveFilePath = _saveDirectoryPath + SaveName + _lastTime + SaveFileFormat;
		}
	}
}
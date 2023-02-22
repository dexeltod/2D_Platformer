using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Infrastructure.Data;
using Infrastructure.Data.PersistentProgress;
using Infrastructure.GameLoading;
using UnityEngine;

namespace Infrastructure.Services.SaveLoadService
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly string _filePath;
        private readonly GameProgress _gameProgress;

        public SaveLoadService()
        {
            _filePath = Application.persistentDataPath + "/Save.dat";
            _gameProgress = ServiceLocator.Container.GetSingle<IPersistentProgressService>().GameProgress;
        }

        public void SaveProgress()
        {
            using (FileStream file = File.Create(_filePath))
            {
                new BinaryFormatter().Serialize(file, _gameProgress);
            }
        }

        public GameProgress LoadProgress()
        {
	        return null;

	        //TODO Need create progress
	        
	        // if (File.Exists(_filePath) == false)
	        //     return null;
	        //
	        // using FileStream file = File.Open(_filePath, FileMode.Open);
	        // {
	        //     object loadedData = new BinaryFormatter().Deserialize(file);
	        //     GameProgress gameProgress = (GameProgress)loadedData;
	        //     return gameProgress;
	        // }
        }
    }
}
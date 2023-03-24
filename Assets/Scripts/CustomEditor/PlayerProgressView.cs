using Infrastructure.Data.PersistentProgress;
using Infrastructure.Data.Serializable;
using Infrastructure.GameLoading;
using Infrastructure.Services.SaveLoadService;
using UnityEngine;

namespace CustomEditor
{
	public class PlayerProgressView : MonoBehaviour
	{
		private PlayerProgressController _playerProgressController;

		private void Awake()
		{
			_playerProgressController = new();
		}

		public void SetStartProgress()
		{
			_playerProgressController.SetStartProgress();
		}
	}

	public class PlayerProgressController
	{
		private GameProgress _gameProgress;

		public void SetStartProgress()
		{
		}
	}
}
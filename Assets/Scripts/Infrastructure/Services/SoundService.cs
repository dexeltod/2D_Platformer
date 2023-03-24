using Game.Sound.Music;
using Infrastructure.GameLoading.AssetManagement;
using UnityEngine;

namespace Infrastructure.Services
{
	public class SoundService : ISoundService
	{
		private readonly SoundSetter _soundSetter;
		private readonly IAssetProvider _assetProvider;
		private string _currentSound;

		public SoundService(SoundSetter soundSetter, IAssetProvider assetProvider)
		{
			_soundSetter = soundSetter;
			_assetProvider = assetProvider;
		}

		public async void Set(string audioName)
		{
			if (_currentSound == audioName || string.IsNullOrWhiteSpace(audioName) == true)
				return;
			
			AudioClip sound =  await _assetProvider.LoadAsync<AudioClip>(audioName);
			_soundSetter.SetAudioClip(sound);
			_currentSound = audioName;
		}

		public void Stop() => 
			_soundSetter.Stop();
	}
}
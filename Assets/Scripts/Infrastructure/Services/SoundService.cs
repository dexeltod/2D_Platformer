using Infrastructure.GameLoading.AssetManagement;
using UnityEngine;

namespace Infrastructure.Services
{
	public class SoundService : ISoundService
	{
		private readonly SoundSetter _soundSetter;
		private readonly IAssetProvider _assetProvider;

		public SoundService(SoundSetter soundSetter, IAssetProvider assetProvider)
		{
			_soundSetter = soundSetter;
			_assetProvider = assetProvider;
		}

		public async void Set(string audioName)
		{
			AudioClip sound =  await _assetProvider.LoadAsync<AudioClip>(audioName);
			_soundSetter.SetAudioClip(sound);
		}

		public void Stop() => 
			_soundSetter.Stop();
	}
}
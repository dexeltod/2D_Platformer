﻿using Cysharp.Threading.Tasks;
using Game.Sound.Music;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Infrastructure.Services
{
	public class MusicService : IMusicService
	{
		private readonly SoundSetter _soundSetter;
		private readonly IAssetProvider _assetProvider;
		private string _currentSound;
		private AudioClip _sound;

		public MusicService(SoundSetter soundSetter, IAssetProvider assetProvider)
		{
			_soundSetter = soundSetter;
			_assetProvider = assetProvider;
		}

		public async UniTask Set(string audioName)
		{
			if (_currentSound == audioName || string.IsNullOrWhiteSpace(audioName) == true)
				return;
			
			_sound = await _assetProvider.LoadAsyncWithoutCash<AudioClip>(audioName);
			_soundSetter.SetAudioClip(_sound);
			_currentSound = audioName;
		}

		public void Stop() =>
			_soundSetter.Stop();
	}
}
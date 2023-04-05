using System;
using UnityEngine;

namespace Game.Sound.Music
{
	public class SoundSetter : MonoBehaviour
	{
		[SerializeField] private AudioSource _audio;
		private AudioClip _clip;
		
		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}

		public void SetAudioClip(AudioClip clip)
		{
			_clip = clip;
			_audio.clip = _clip;
			_audio.Play();
		}

		public void Stop()
		{
			if (_audio.isPlaying)
				_audio.Stop();
		}
	}
}
using Game.Sound.Music;
using Infrastructure.States;
using UI_Scripts.Curtain;
using UnityEngine;

namespace Infrastructure.GameLoading
{
	public class Bootstrapper : MonoBehaviour, ICoroutineRunner
	{
		[SerializeField] private LoadingCurtain _loadingCurtain;
		[SerializeField] private SoundSetter _soundSetter;
		
		private Game _game;

		private void Awake()
		{
			SoundSetter soundSetter = GetMusicSetter();
			var loadingCurtain = GetLoadingCurtain();
			loadingCurtain.gameObject.SetActive(false);
			
			_game = new Game(this, loadingCurtain, soundSetter);
			_game.StateMachine.Enter<InitializeServicesState>();
			
			DontDestroyOnLoad(this);
		}

		private SoundSetter GetMusicSetter() =>
			Instantiate(_soundSetter);
		
		private LoadingCurtain GetLoadingCurtain() => 
			Instantiate(_loadingCurtain);
	}
}
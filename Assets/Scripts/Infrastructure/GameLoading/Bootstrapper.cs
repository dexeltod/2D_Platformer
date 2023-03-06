using Infrastructure.States;
using UI_Scripts.Curtain;
using UnityEngine;

namespace Infrastructure.GameLoading
{
	public class Bootstrapper : MonoBehaviour, ICoroutineRunner
	{
		[SerializeField] private LoadingCurtain _loadingCurtain;

		private Game _game;

		private void Awake()
		{
			var loadingCurtain = GetLoadingCurtain();
			loadingCurtain.gameObject.SetActive(false);
			
			_game = new Game(this, loadingCurtain);
			_game.StateMachine.Enter<BootstrapState>();

			DontDestroyOnLoad(this);
		}

		private LoadingCurtain GetLoadingCurtain() => 
			Instantiate(_loadingCurtain);
	}
}
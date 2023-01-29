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
			_game = new Game(this, Instantiate(_loadingCurtain));
			_game.StateMachine.Enter<BootstrapState>();

			DontDestroyOnLoad(this);
		}
	}
}
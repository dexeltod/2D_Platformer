using System;
using Infrastructure;
using UI_Scripts.Curtain;
using UnityEngine;

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
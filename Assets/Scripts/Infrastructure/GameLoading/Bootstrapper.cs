using System;
using Infrastructure;
using UI_Scripts.Curtain;
using UnityEngine;

public class Bootstrapper : MonoBehaviour, ICoroutineRunner
{
	[SerializeField] private LoadingCurtain _loadingCurtain;

	[Header("Game resolution")] [SerializeField]
	private int _height;

	[SerializeField] private int _width;
	[SerializeField] private bool _isFullscreen;
	[SerializeField] private bool _isEnableChangingResolution;

	private Game _game;

	private void Awake()
	{
		if (_isEnableChangingResolution)
			Screen.SetResolution(_width, _height, _isFullscreen);
		
		_game = new Game(this, Instantiate(_loadingCurtain));
		_game.StateMachine.Enter<BootstrapState>();

		DontDestroyOnLoad(this);
	}
}
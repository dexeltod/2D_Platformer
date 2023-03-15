using System;
using Game.PlayerScripts;
using Infrastructure.GameLoading;
using Infrastructure.Services;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Environment.EnterTriggers.Enter
{
	public class EnterInLocationByButtonTrigger : MonoBehaviour
	{
		[FormerlySerializedAs("_levelConfig")] [SerializeField] private SceneConfig _sceneConfig;

		public event Action<bool> InTriggerEntered;

		private IGameStateMachine _gameStateMachine;
		private IInputService _inputService;

		private void Awake()
		{
			_inputService = ServiceLocator.Container.GetSingle<IInputService>();
			_gameStateMachine = ServiceLocator.Container.GetSingle<IGameStateMachine>();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.TryGetComponent(out Player _))
			{
				InTriggerEntered?.Invoke(true);
				_inputService.InteractButtonUsed += OnChangeLevel;
			}
		}

		private void OnChangeLevel()
		{
			_gameStateMachine.Enter<SceneLoadState, string, bool>(_sceneConfig.Name, _sceneConfig.IsStopMusicBetweenScenes, _sceneConfig.Music);
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.TryGetComponent(out Player _))
			{
				InTriggerEntered?.Invoke(false);
				_inputService.InteractButtonUsed -= OnChangeLevel;
			}
		}
	}
}
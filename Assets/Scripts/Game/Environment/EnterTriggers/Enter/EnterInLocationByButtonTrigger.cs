using System;
using Game.PlayerScripts;
using Infrastructure.GameLoading;
using Infrastructure.Services;
using Infrastructure.States;
using UnityEngine;

namespace Game.Environment.EnterTriggers.Enter
{
	public class EnterInLocationByButtonTrigger : MonoBehaviour
	{
		[SerializeField] private string _levelName;

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
				_inputService.InteractButtonUsed += ChangeLevel;
			}
		}

		private void ChangeLevel() => 
			_gameStateMachine.Enter<SceneLoadState, string>(_levelName);

		private void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.TryGetComponent(out Player _))
			{
				InTriggerEntered?.Invoke(false);
				_inputService.InteractButtonUsed -= ChangeLevel;
			}
		}
	}
}
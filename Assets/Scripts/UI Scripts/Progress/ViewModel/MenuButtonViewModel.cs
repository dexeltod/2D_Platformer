using System;
using Game.SceneConfigs;
using Infrastructure.GameLoading;
using Infrastructure.States;
using UI_Scripts.View;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI_Scripts.ViewModel
{
	[RequireComponent(typeof(MenuButtonView))]
	public class MenuButtonViewModel : MonoBehaviour
	{
		[SerializeField] private SceneConfig _sceneConfig;
		[SerializeField] private MenuButtonView _buttonView;
		private IGameStateMachine _gameStateMachine;

		private void Awake()
		{
			_gameStateMachine = ServiceLocator.Container.GetSingle<IGameStateMachine>();
		}

		private void OnEnable()
		{
			_buttonView.PlayButtonPressed += PlayButtonPressed;
		}

		private void OnDisable()
		{
			_buttonView.PlayButtonPressed -= PlayButtonPressed;
		}

		private void PlayButtonPressed()
		{
			
			_gameStateMachine.Enter<SceneLoadState, string, bool>(_sceneConfig.Name, _sceneConfig.IsStopMusicBetweenScenes, _sceneConfig.Music);
		}
	}
}
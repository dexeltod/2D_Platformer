

using Game.SceneConfigs;
using Infrastructure.GameLoading;
using Infrastructure.States;
using UI_Scripts.View;
using UnityEngine;

namespace UI_Scripts.ViewModel
{
	public class PlayButtonViewModel : MonoBehaviour
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
			_buttonView.ButtonPressed += OnButtonPressed;
		}

		private void OnDisable()
		{
			_buttonView.ButtonPressed -= OnButtonPressed;
		}

		private void OnButtonPressed()
		{
			
			_gameStateMachine.Enter<SceneLoadState, string, bool>(_sceneConfig.Name, _sceneConfig.IsStopMusicBetweenScenes, _sceneConfig.Music);
		}
	}
}
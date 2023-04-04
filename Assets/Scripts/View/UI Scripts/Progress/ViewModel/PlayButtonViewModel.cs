using Game.SceneConfigs;
using Infrastructure.GameLoading;
using Infrastructure.Services.Interfaces;
using Infrastructure.States;
using UnityEngine;
using View.UI_Scripts.Progress.View;

namespace View.UI_Scripts.Progress.ViewModel
{
	public class PlayButtonViewModel : MonoBehaviour
	{
		[SerializeField] private SceneConfig _sceneConfig;
		[SerializeField] private MenuButtonView _buttonView;

		private IGameStateMachine _gameStateMachine;

		private void Awake() => 
			_gameStateMachine = ServiceLocator.Container.GetSingle<IGameStateMachine>();

		private void OnEnable() => 
			_buttonView.ButtonPressed += OnButtonPressed;

		private void OnDisable() => 
			_buttonView.ButtonPressed -= OnButtonPressed;

		private void OnButtonPressed() =>
			_gameStateMachine.Enter<SceneLoadState, string, bool>(_sceneConfig.SceneName, _sceneConfig.MusicName,
				_sceneConfig.IsStopMusicBetweenScenes);
	}
}
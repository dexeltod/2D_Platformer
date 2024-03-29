using Game.SceneConfigs;
using Infrastructure.GameLoading;
using Infrastructure.Services.Interfaces;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace View.StartMenu.View
{
	[RequireComponent(typeof(Button))]
	public class LevelSelectButton : MonoBehaviour
	{
		[SerializeField] private SceneConfig _sceneConfig;

		private Button _button;
		private IGameStateMachine _stateMachine;

		private void Awake()
		{
			_button = GetComponent<Button>();
			_stateMachine = ServiceLocator.Container.GetSingle<IGameStateMachine>();
		}

		private void OnEnable() =>
			_button.onClick.AddListener(LoadLevel);

		private void OnDisable() =>
			_button.onClick.RemoveListener(LoadLevel);

		private void LoadLevel() =>
			_stateMachine.Enter<SceneLoadState, string, bool>(_sceneConfig.SceneName, _sceneConfig.MusicName,
				_sceneConfig.IsStopMusicBetweenScenes);
	}
}
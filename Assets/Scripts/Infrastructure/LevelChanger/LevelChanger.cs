using Game.PlayerScripts;
using Infrastructure.GameLoading;
using Infrastructure.States;
using UnityEngine;

namespace Infrastructure.LevelChanger
{
	public class LevelChanger : MonoBehaviour
	{
		[SerializeField] private string _levelName;

		private IGameStateMachine _gameStateMachine;

		private void Start()
		{
			_gameStateMachine = ServiceLocator.Container.GetSingle<IGameStateMachine>();
		}

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (col.TryGetComponent(out Player _)) 
				_gameStateMachine.Enter<SceneLoadState, string>(_levelName);
		}
	}
}


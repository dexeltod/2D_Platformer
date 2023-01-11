using Infrastructure;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
	[SerializeField] private string _levelName;

	private IGameStateMachine _gameStateMachine;

	private void Start()
	{
		_gameStateMachine = ServiceLocator.Container.Single<IGameStateMachine>();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.TryGetComponent(out Player _)) 
			_gameStateMachine.Enter<SceneLoadState, string>(_levelName);
	}
}


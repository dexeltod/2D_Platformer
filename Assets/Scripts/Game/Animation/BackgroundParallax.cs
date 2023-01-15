using Cinemachine;
using Infrastructure;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera _camera;
	private Transform _followedTarget;

	private IGameFactory _gameFactory;
	private Vector2 _startPosition;
	private float _startZ;

	private Vector2 _travel => (Vector2)_camera.transform.position - _startPosition;
	private float _distanceFromTarget => transform.position.z - _followedTarget.transform.position.z;

	private void Start()
	{
		_startPosition = transform.position;
		_startZ = transform.position.z;

		_gameFactory = ServiceLocator.Container.Single<IGameFactory>();
		_gameFactory.MainCharacterCreated += OnSceneLoaded;
	}

	private void OnSceneLoaded() => 
		_followedTarget = _gameFactory.MainCharacter.transform;

	private void FixedUpdate()
	{
		Vector2 currentPosition = _startPosition + _travel * _distanceFromTarget;

		transform.position = new Vector3(currentPosition.x, _camera.transform.position.y, _startZ);
	}
}
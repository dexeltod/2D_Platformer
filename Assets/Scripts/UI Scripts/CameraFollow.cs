using Infrastructure;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private float _height;
	[SerializeField] private float _closeness = -23;
	private Transform _player;

	private IGameFactory _gameFactory;

	private void Start()
	{
		_gameFactory = ServiceLocator.Container.Single<IGameFactory>();
		_gameFactory.MainCharacterCreated += Initialize;
	}

	private void FixedUpdate() =>
		UpdateCameraPosition();

	private void Initialize()
	{
		_gameFactory.MainCharacterCreated -= Initialize;
		_player = _gameFactory.MainCharacter.transform;
	}

	private void UpdateCameraPosition()
	{
		if(_player == null)
			return;
		
		Vector3 position = new Vector3(_player.position.x, _player.position.y + _height, _closeness);
		transform.position = position;
	}
}
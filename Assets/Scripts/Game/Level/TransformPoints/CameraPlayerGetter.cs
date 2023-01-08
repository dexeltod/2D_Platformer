using Cinemachine;
using Infrastructure;
using UnityEngine;

public class CameraPlayerGetter : MonoBehaviour
{
	private Transform _player;
	private CinemachineVirtualCamera _cinemachineVirtualCamera;

	private IGameFactory _gameFactory;

	private void Awake()
	{
		_cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
		_gameFactory = ServiceLocator.Container.Single<IGameFactory>();
		_gameFactory.MainCharacterCreated += Initialize;
	}

	private void Initialize()
	{
		_gameFactory.MainCharacterCreated -= Initialize;
		_player = _gameFactory.MainCharacter.transform;
		_cinemachineVirtualCamera.Follow = _player.transform;
		_cinemachineVirtualCamera.LookAt = _player.transform;
	}
}

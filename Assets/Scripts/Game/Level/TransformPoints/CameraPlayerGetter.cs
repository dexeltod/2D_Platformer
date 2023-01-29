using Cinemachine;
using Infrastructure.GameLoading;
using UnityEngine;

namespace Game.Level.TransformPoints
{
    public class CameraPlayerGetter : MonoBehaviour
    {
        private Transform _player;
        private CinemachineVirtualCamera _cinemachineVirtualCamera;

        private IPlayerFactory _playerFactory;

        private void Awake()
        {
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            _playerFactory = ServiceLocator.Container.GetSingle<IPlayerFactory>();
            _playerFactory.MainCharacterCreated += Initialize;
        }

        private void Initialize()
        {
            _playerFactory.MainCharacterCreated -= Initialize;
            _player = _playerFactory.MainCharacter.transform;
            _cinemachineVirtualCamera.Follow = _player;
            _cinemachineVirtualCamera.LookAt = _player;
        }
    }
}

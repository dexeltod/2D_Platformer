using Cinemachine;
using Infrastructure.GameLoading;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Game.Level.TransformPoints
{
    public class CameraPlayerGetter : MonoBehaviour
    {
        private Transform _player;
        private CinemachineVirtualCamera _cinemachineVirtualCamera;

        private ISceneLoadInformer _sceneLoadInformer;
        private IPlayerFactory _playerFactory;

        private void Awake()
        {
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            _sceneLoadInformer = ServiceLocator.Container.GetSingle<ISceneLoadInformer>();
            _sceneLoadInformer.SceneLoaded += Initialize;
        }

        private void Initialize()
        {
	        _sceneLoadInformer.SceneLoaded -= Initialize;
	        
            _playerFactory = ServiceLocator.Container.GetSingle<IPlayerFactory>();
            _player = _playerFactory.MainCharacter.transform;
            _cinemachineVirtualCamera.Follow = _player;
            _cinemachineVirtualCamera.LookAt = _player;
        }
    }
}

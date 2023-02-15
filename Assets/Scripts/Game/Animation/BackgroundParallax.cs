using Infrastructure.GameLoading;
using Infrastructure.GameLoading.Factory;
using UnityEngine;

namespace Game.Animation
{
    public class BackgroundParallax : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        private Transform _followedTarget;

        private IPlayerFactory _playerFactory;
        private Vector2 _startPosition;
        private float _startZ;

        private Vector2 _travel => (Vector2)_camera.transform.position - _startPosition;
        private float _distanceFromTarget => transform.position.z - _followedTarget.transform.position.z;

        private void Start()
        {
            _playerFactory = ServiceLocator.Container.GetSingle<IPlayerFactory>();
            _startPosition = transform.position;
            _startZ = transform.position.z;

            _playerFactory.MainCharacterCreated += OnSceneLoaded;
        }

        private void OnSceneLoaded() =>
            _followedTarget = _playerFactory.MainCharacter.transform;

        private void FixedUpdate()
        {
            if (_followedTarget == null)
                return;

            Vector2 currentPosition = _startPosition + _travel * _distanceFromTarget;

            transform.position = new Vector3(currentPosition.x, _camera.transform.position.y, _startZ);
        }
    }
}
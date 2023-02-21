using Infrastructure.GameLoading;
using Infrastructure.GameLoading.Factory;
using Infrastructure.Services;
using UnityEngine;

namespace Game.Animation
{
    public class BackgroundParallax : MonoBehaviour
    {
        private ICamera _cameraInstance;
        
        private Transform _followedTarget;

        private IPlayerFactory _playerFactory;
        private Vector2 _startPosition;
        private ISceneLoadInformer _levelLoadInformer;

        private Vector2 _direction;

        private float _startZ;
        private float _distanceFromTarget;
        private float _clippingPlane;
        private float _parallaxFactor;
        private Camera _camera;

        private void Awake() 
        {
            _levelLoadInformer = ServiceLocator.Container.GetSingle<ISceneLoadInformer>();
            _levelLoadInformer.SceneLoaded += OnSceneLoaded;
	        _playerFactory = ServiceLocator.Container.GetSingle<IPlayerFactory>();
	        
            _startPosition = transform.position;
            _startZ = transform.position.z;
        }

        private void OnSceneLoaded()
        {
	        _camera = ServiceLocator.Container.GetSingle<ICamera>().Camera;
	        _followedTarget = _playerFactory.MainCharacter.transform;
	        _levelLoadInformer.SceneLoaded -= OnSceneLoaded;
        }

        private void Update()
        {
            if (_followedTarget == null)
                return;

            var parallaxPosition = GetParallaxPosition();
            transform.position = new Vector3(parallaxPosition.x, _followedTarget.position.y, _startZ);
        }

        private Vector2 GetParallaxPosition()
        {
	        _direction = GetDirection();
	        _distanceFromTarget = GetDistanceFromTarget();
	        CountClippingPlane();
	        CountParallaxFactor();

	        Vector2 currentPosition = _startPosition + _direction * _parallaxFactor;
	        return currentPosition;
        }

        private void CountClippingPlane() =>
	        _clippingPlane = (_camera.transform.position.z +
	                          (_distanceFromTarget > 0 ? _camera.farClipPlane : _camera.nearClipPlane));

        private float GetDistanceFromTarget() => 
	        transform.position.z - _followedTarget.transform.position.z;

        private Vector2 GetDirection() => 
	        (Vector2)_camera.transform.position - _startPosition;

        private void CountParallaxFactor() => 
	        _parallaxFactor = Mathf.Abs(_distanceFromTarget) / _clippingPlane;
    }
}
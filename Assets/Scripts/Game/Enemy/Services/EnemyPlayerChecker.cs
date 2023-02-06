using System;
using System.Collections.Generic;
using Game.Level.TransformPoints;
using Infrastructure.GameLoading;
using UnityEngine;

namespace Game.Enemy.Services
{
    public class EnemyPlayerChecker : MonoBehaviour
    {
        [SerializeField] private Transform _eyeTransform;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _viewDistance;

        [SerializeField] private float _minAngleView = 68;
        [SerializeField] private float _maxAngleView = 230;

        private float _angleView;

        private IPlayerFactory _factory;
        private Transform _playerTransform;
        private List<RaycastHit2D> _hits;
        private Vector2 _directionToTarget;

        private bool _lastSawPlayer;
        private bool _isSawPlayer;
        private float _distanceToTarget;
        private float _currentAngle;

        public bool CanSeePlayer { get; private set; }

        public float ViewDistance => _viewDistance;
        public Transform EyeTransform => _eyeTransform;
        public float AngleView => _angleView;
        public Transform PlayerTransform => _playerTransform;
        
        public event Action<bool> SeenPlayer;

        private void Start()
        {
            _angleView = _minAngleView;
            _factory = ServiceLocator.Container.GetSingle<IPlayerFactory>();
            _factory.MainCharacterCreated += OnLevelLoaded;
        }

        private void OnLevelLoaded()
        {
            _playerTransform = _factory.MainCharacter.GetComponentInChildren<PlayerEyePoint>().transform;
            _factory.MainCharacterCreated -= OnLevelLoaded;
        }

        private void Update() =>
            CheckTargetVisibility();

        private void CheckTargetVisibility()
        {
            Collider2D[] range = Physics2D.OverlapCircleAll(_eyeTransform.position, _viewDistance, _playerLayer);

            bool isInRange = range.Length > 0;

            if (isInRange == true)
            {
                Transform target = GetTargetFromRange(range);
                CountTargetVisibility(target);
            }
            else
                SetTargetVisibility(false);
        }

        private void CountTargetVisibility(Transform target)
        {
            const float HalfOfAngle = 2;

            if (_currentAngle < _angleView / HalfOfAngle)
            {
                _distanceToTarget = Vector2.Distance(_eyeTransform.position, target.position);

                CastRayToTarget(_distanceToTarget);

                if (_isSawPlayer == _lastSawPlayer)
                    return;

                SetTargetVisibility(_isSawPlayer);
            }
        }

        private void SetTargetVisibility(bool isSeeTarget)
        {
            _lastSawPlayer = isSeeTarget;
            _angleView = isSeeTarget ? _maxAngleView : _minAngleView;
            CanSeePlayer = isSeeTarget;
            SeenPlayer?.Invoke(isSeeTarget);
        }

        private void CastRayToTarget(float distance) =>
            _isSawPlayer = Physics2D.Raycast(_eyeTransform.position, _directionToTarget, distance, _groundLayer) == false;

        private Transform GetTargetFromRange(Collider2D[] range)
        {
            const int FirstCollidedObject = 0;

            Transform target = range[FirstCollidedObject].transform;
            CountTargetPosition(target);
            return target;
        }

        private void CountTargetPosition(Transform target)
        {
            _directionToTarget = (target.position - _eyeTransform.position).normalized;
            _currentAngle = Vector2.Angle(_eyeTransform.right, _directionToTarget);
        }
    }
}
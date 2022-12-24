using System;
using System.Collections.Generic;
using Infrastructure;
using UnityEngine;

public class EnemyPlayerChecker : MonoBehaviour
{
	[SerializeField] private Transform _eyeTransform;
	[SerializeField] private LayerMask _playerLayer;
	[SerializeField] private float _viewDistance;

	[SerializeField] private float _angleView;

	private IGameFactory _factory;
	private Transform _playerTransform;
	private List<RaycastHit2D> _hits;

	private bool _inRange;
	private bool _lastSawPlayer;
	private bool _sawPlayer;

	public bool CanSeePlayer { get; private set; }

	public float ViewDistance => _viewDistance;
	public Transform EyeTransform => _eyeTransform;
	public Transform PlayerTransform => _playerTransform;
	public float AngleView => _angleView;

	public event Action<bool> SeenPlayer;

	private void Start()
	{
		_factory = ServiceLocator.Container.Single<IGameFactory>();
		_factory.MainCharacterCreated += OnLevelLoaded;
	}

	private void OnLevelLoaded()
	{
		_playerTransform = _factory.MainCharacter.transform;
		_factory.MainCharacterCreated -= OnLevelLoaded;
	}

	private void Update()
	{
		if (_inRange == true)
			IsInView();
	}

	private void IsInView()
	{
		Collider2D[] range = Physics2D.OverlapCircleAll(_eyeTransform.position, _viewDistance, _playerLayer);

		if (range.Length > 0)
		{
			const int FirstCollidedObject = 0;

			Transform target = range[FirstCollidedObject].transform;

			Vector2 directionToTarget = (target.position - _eyeTransform.position).normalized;

			if (Vector2.Angle(_eyeTransform.right, directionToTarget) < _angleView / 2)
			{
				float distanceToTarget = Vector2.Distance(_eyeTransform.position, target.position);

				_sawPlayer = Physics2D.Raycast(_eyeTransform.position, directionToTarget, distanceToTarget,
					_playerLayer);

				CanSeePlayer = _sawPlayer;

				if (_sawPlayer == _lastSawPlayer)
					return;

				_lastSawPlayer = _sawPlayer;
				SeenPlayer?.Invoke(_lastSawPlayer);
			}
		}
	}
}
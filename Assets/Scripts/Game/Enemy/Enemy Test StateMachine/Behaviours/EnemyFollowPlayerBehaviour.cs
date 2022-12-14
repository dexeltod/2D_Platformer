using System;
using Infrastructure;
using UnityEngine;

[RequireComponent(typeof(AnimationHasher), typeof(Animator), typeof(Rigidbody2D))]
public class EnemyFollowPlayerBehaviour : MonoBehaviour
{
	[SerializeField] private EnemyData _enemyData;

	private Player _player;
	private EnemyObserver _enemyObserver;
	private Rigidbody2D _rigidbody2D;
	private Vector2 _direction;
	private Animator _animator;
	private AnimationHasher _animationHasher;

	private bool _isRotateRight;
	private bool _lastRotateRight;

	private Vector2 _followDirection = new(-1, 0);
	private Vector2 _targetDirection;

	private float _lastFollowDirection;
	private IGameFactory _gameFactory;

	public event Action PlayerIsAbove;

	private void Awake()
	{
		_gameFactory = ServiceLocator.Container.Single<IGameFactory>();
		_enemyObserver = GetComponent<EnemyObserver>();
		_animator = GetComponent<Animator>();
		_animationHasher = GetComponent<AnimationHasher>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_gameFactory.MainCharacterCreated += OnLevelLoaded;
	}

	private void OnLevelLoaded()
	{
		_player = _gameFactory.MainCharacter.GetComponent<Player>();
		_gameFactory.MainCharacterCreated -= OnLevelLoaded;
	}

	private void OnEnable()
	{
		_enemyObserver.PlayerIsAbove += OnPlayerAbove;
		_animator.StopPlayback();
		_animator.CrossFade(_animationHasher.RunHash, 0);
	}

	private void OnDisable()
	{
		_enemyObserver.PlayerIsAbove -= OnPlayerAbove;
		_animator.StopPlayback();
	}

	private void FixedUpdate()
	{
		_targetDirection = _player.transform.position - transform.position;
		_rigidbody2D.position += _followDirection * (_enemyData.RunSpeed * Time.deltaTime);
		CheckDirectionToRotate();
	}

	private void OnPlayerAbove() => 
		PlayerIsAbove.Invoke();

	private void CheckDirectionToRotate()
	{
		_followDirection.x = _targetDirection.x > 0 ? 1 : -1;

		if (_lastFollowDirection == _followDirection.x)
			return;

		_lastFollowDirection = _followDirection.x;
		_enemyObserver.SetFacingDirection(_followDirection.x);
	}
}
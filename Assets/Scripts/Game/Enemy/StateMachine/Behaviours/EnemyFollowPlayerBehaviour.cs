using Game.Animation.AnimationHashes.Characters;
using Game.CharactersSettingsSO.Characters.Enemy;
using Game.Enemy.Services;
using Game.PlayerScripts;
using Infrastructure.GameLoading;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Game.Enemy.StateMachine.Behaviours
{
	[RequireComponent(typeof(AnimationHasher), typeof(Animator), typeof(Rigidbody2D))]
	[RequireComponent(typeof(EnemyObserver))]
	public class EnemyFollowPlayerBehaviour : MonoBehaviour
	{
		[SerializeField] private EnemyData _enemyData;

		private IPlayerFactory _playerFactory;
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
		private ISceneLoadInformer _sceneLoadInformer;
		private bool _isPlayerNotNull;

		private void Start()
		{
			_isPlayerNotNull = _player != null;
		}

		private void Awake()
		{
			_playerFactory = ServiceLocator.Container.GetSingle<IPlayerFactory>();
			_sceneLoadInformer = ServiceLocator.Container.GetSingle<ISceneLoadInformer>();
			_enemyObserver = GetComponent<EnemyObserver>();
			_animator = GetComponent<Animator>();
			_animationHasher = GetComponent<AnimationHasher>();
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_sceneLoadInformer.SceneLoaded += OnCharacterCreated;
		}

		private void OnCharacterCreated()
		{
			_player = _playerFactory.MainCharacter.GetComponent<Player>();
			_sceneLoadInformer.SceneLoaded -= OnCharacterCreated;
		}

		private void OnEnable() => 
			_animator.Play(_animationHasher.RunHash);

		private void OnDisable() => 
			_rigidbody2D.position += Vector2.zero;

		private void FixedUpdate()
		{
			if (_isPlayerNotNull)
				_targetDirection = _player.transform.position - transform.position;
			
			_rigidbody2D.position += _followDirection * (_enemyData.RunSpeed * Time.deltaTime);
			CheckDirectionToRotate();
		}

		private void CheckDirectionToRotate()
		{
			_followDirection.x = _targetDirection.x > 0 
				? 1 
				: -1;

			if (_lastFollowDirection == _followDirection.x)
				return;

			_lastFollowDirection = _followDirection.x;
			_enemyObserver.SetFacingDirection(_followDirection.x);
		}
	}
}
using System;
using System.Collections;
using Infrastructure;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AnimationHasher), typeof(Animator))]
public class EnemyAttackBehaviour : MonoBehaviour
{
	[SerializeField] private EnemyData _enemyData;
	[SerializeField] private EnemyMeleePlayerChecker _enemyMeleePlayerChecker;

	private PlayerHealth _playerHealth;
	private Animator _animator;
	private AnimationHasher _animationHasher;
	private Coroutine _currentCoroutine;
	private bool _canAttack;
	private IGameFactory _gameFactory;

	private void Awake()
	{
		_gameFactory = ServiceLocator.Container.Single<IGameFactory>();
		_animator = GetComponent<Animator>();
		_animationHasher = GetComponent<AnimationHasher>();
		_gameFactory.MainCharacterCreated += OnLevelLoaded;
	}

	private void OnLevelLoaded()
	{
		_playerHealth = _gameFactory.MainCharacter.GetComponent<PlayerHealth>();
		_gameFactory.MainCharacterCreated -= OnLevelLoaded;
	}
	
	private void OnEnable()
	{
		_canAttack = true;
		OnAttack(_canAttack);
		_enemyMeleePlayerChecker.TouchedPlayer += OnAttack;
	}

	private void OnDisable()
	{
		_enemyMeleePlayerChecker.TouchedPlayer -= OnAttack;
		_animator.StopPlayback();

		if (_currentCoroutine != null)
			StopCoroutine(_currentCoroutine);
	}

	public void Initialize(PlayerHealth playerHealth) =>
		_playerHealth = playerHealth;

	private void OnAttack(bool canAttack)
	{
		_canAttack = canAttack;

		if (_currentCoroutine != null)
		{
			StopCoroutine(_currentCoroutine);
			_currentCoroutine = null;
		}

		if (_canAttack == true)
			_currentCoroutine = StartCoroutine(AttackPlayer());
	}

	private IEnumerator AttackPlayer()
	{
		SetAnimatorSettings();
		var waitingTime = new WaitForSeconds(GetAnimationSpeed());

		while (_canAttack == true)
		{
			_playerHealth.ApplyDamage(_enemyData.Damage);
			yield return waitingTime;
		}
	}

	private void SetAnimatorSettings()
	{
		_animator.Play(_animationHasher.AttackHash);
		_animator.SetFloat(_animationHasher.AttackSpeedHash, _enemyData.AttackSpeed);
	}

	private float GetAnimationSpeed()
	{
		int currentLayer = 0;
		var stateInfo = _animator.GetCurrentAnimatorStateInfo(currentLayer);
		return stateInfo.length;
	}
}
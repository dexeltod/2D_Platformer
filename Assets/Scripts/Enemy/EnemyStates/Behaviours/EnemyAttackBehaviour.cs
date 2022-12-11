using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AnimationHasher), typeof(Animator))]
public class EnemyAttackBehaviour : MonoBehaviour
{
	[SerializeField] private DataEnemy _enemyData;
	[SerializeField] private PlayerHealth _playerHealth;
	[SerializeField] private EnemyMeleeRangeInformer _enemyMeleeRangeInformer;

	private Animator _animator;
	private AnimationHasher _animationHasher;
	private Coroutine _currentCoroutine;
	private bool _canAttack;

	public event UnityAction PlayerDied;
	public event UnityAction PlayerOutOfRangeAttack;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_animationHasher = GetComponent<AnimationHasher>();
	}

	private void OnEnable()
	{
		_canAttack = true;
		OnAttack(_canAttack);
		_enemyMeleeRangeInformer.TouchedPlayer += OnAttack;
		_playerHealth.Died += OnPlayerHealthDie;
	}

	private void OnDisable()
	{
		_enemyMeleeRangeInformer.TouchedPlayer -= OnAttack;
		_animator.StopPlayback();
		_playerHealth.Died -= OnPlayerHealthDie;

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
		else
			PlayerOutOfRangeAttack.Invoke();
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

	private void OnPlayerHealthDie() =>
		PlayerDied?.Invoke();
}
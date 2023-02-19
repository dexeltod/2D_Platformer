using Game.Animation.AnimationHashes.Characters;
using Game.Enemy.EnemySettings.TestEnemy.Data.ScriptableObjects;
using Game.Enemy.Services;
using Game.PlayerScripts.PlayerData;
using Infrastructure.GameLoading;
using Infrastructure.GameLoading.Factory;
using UnityEngine;

namespace Game.Enemy.StateMachine.Behaviours
{
	[RequireComponent(typeof(AnimationHasher), typeof(Animator))]
	
	public class EnemyAttackBehaviour : MonoBehaviour
	{
		[SerializeField] private EnemyData _enemyData;
		[SerializeField] private EnemyAttackTrigger _enemyAttackTrigger;
		[SerializeField] private GameObject _enemyAttackGameObject;
		[SerializeField] private EnemyMeleeTrigger _enemyMeleeTrigger;

		private bool _canAttack;
		private PlayerHealth _playerHealth;
		private Animator _animator;
		private AnimationHasher _animationHasher;
		private Coroutine _currentCoroutine;
		private IPlayerFactory _playerFactory;
		private WaitForSeconds _waitForSeconds;

		private void Awake()
		{
			_playerFactory = ServiceLocator.Container.GetSingle<IPlayerFactory>();
			_animator = GetComponent<Animator>();
			_animationHasher = GetComponent<AnimationHasher>();
			_playerFactory.MainCharacterCreated += OnLevelLoaded;
		}

		private void OnLevelLoaded()
		{
			_playerHealth = _playerFactory.MainCharacter.GetComponent<PlayerHealth>();
			_playerFactory.MainCharacterCreated -= OnLevelLoaded;
		}

		private void OnEnable()
		{
			_enemyAttackGameObject.SetActive(true);
			_enemyAttackTrigger.enabled = true;
			_enemyAttackTrigger.TouchedPlayer += OnGiveDamage;

			PlayAttackAnimation();

			_canAttack = true;
		}

		private void OnDisable()
		{
			_enemyAttackGameObject.SetActive(false);
			_enemyAttackTrigger.TouchedPlayer -= OnGiveDamage;

			_enemyMeleeTrigger.TouchedPlayer -= OnAttack;
			_enemyAttackTrigger.enabled = false;

			_animator.StopPlayback();
		}

		public void Initialize(PlayerHealth playerHealth) =>
			_playerHealth = playerHealth;

		private void OnGiveDamage(bool isTouchedPlayer)
		{
			if (isTouchedPlayer)
				_playerHealth.ApplyDamage(_enemyData.Damage);
		}

		private void OnAttack(bool canAttack)
		{
			_canAttack = canAttack;

			if (_canAttack == true)
				PlayAttackAnimation();
		}

		private void PlayAttackAnimation()
		{
			_animator.Play(_animationHasher.AttackHash);
			_animator.SetFloat(_animationHasher.AttackSpeedHash, _enemyData.AttackSpeed);
		}
	}
}
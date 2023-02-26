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
		[SerializeField] private EnemyMeleeAreaTrigger _enemyMeleeAreaTrigger;
		
		[SerializeField] private EnemyMeleeDamageTrigger _enemyMeleeDamageTrigger;

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
			_enemyMeleeAreaTrigger.TouchedPlayer += OnAttack;
		
			_enemyMeleeDamageTrigger.TouchedPlayer += OnGiveDamage;

			PlayAttackAnimation();

			_canAttack = true;
		}

		private void OnDisable()
		{
			_enemyMeleeDamageTrigger.gameObject.SetActive(false);
			_enemyMeleeDamageTrigger.TouchedPlayer -= OnGiveDamage;

			_enemyMeleeAreaTrigger.TouchedPlayer -= OnAttack;

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
			_enemyMeleeDamageTrigger.gameObject.SetActive(true);
			_animator.Play(_animationHasher.AttackHash);
			_animator.SetFloat(_animationHasher.AttackSpeedHash, _enemyData.AttackSpeed);
		}
	}
}
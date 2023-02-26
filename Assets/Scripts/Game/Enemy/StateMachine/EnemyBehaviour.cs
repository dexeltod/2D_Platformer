using System.Collections.Generic;
using System.Linq;
using Game.Animation.AnimationHashes.Characters;
using Game.Enemy.EnemySettings.TestEnemy.Data.ScriptableObjects;
using Game.Enemy.Services;
using Game.Enemy.StateMachine.Behaviours;
using Game.Enemy.StateMachine.States;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Game.Enemy.StateMachine
{
    [RequireComponent(typeof(ShadowCaster2D), typeof(CapsuleCollider2D), typeof(Rigidbody2D))]
    [RequireComponent(typeof(Enemy), typeof(Animator), typeof(AnimationHasher))]
    [RequireComponent(typeof(EnemyObserver), typeof(EnemyFollowPlayerBehaviour), typeof(EnemyPatrolBehaviour))]
    [RequireComponent(typeof(EnemyAttackBehaviour))]
    
    public class EnemyBehaviour : MonoBehaviour, IEnemyStateSwitcher
    {
        [SerializeField] private EnemyData _enemyData;

        private ShadowCaster2D _shadowCaster2D;
        private CapsuleCollider2D _capsuleCollider2D;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private AnimationHasher _animationHasher;

        private EnemyObserver _enemyObserver;
        private Enemy _enemy;

        private EnemyAttackBehaviour _attackBehaviour;
        private EnemyPatrolBehaviour _enemyPatrolBehaviour;
        private EnemyFollowPlayerBehaviour _enemyFollowBehaviour;

        private List<EnemyStateMachine> _states = new();
        private EnemyStateMachine _currentState;

        private void Awake()
        {
            _shadowCaster2D = GetComponent<ShadowCaster2D>();
            _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _enemy = GetComponent<Enemy>();
            _animator = GetComponent<Animator>();
            _animationHasher = GetComponent<AnimationHasher>();
            _enemyObserver = GetComponent<EnemyObserver>();
            _enemyFollowBehaviour = GetComponent<EnemyFollowPlayerBehaviour>();
            _enemyPatrolBehaviour = GetComponent<EnemyPatrolBehaviour>();
            _attackBehaviour = GetComponent<EnemyAttackBehaviour>();

            InitializeStates();
        }

        private void OnEnable() =>
            _enemy.Dying += OnDie;

        private void OnDisable() =>
            _enemy.Dying -= OnDie;

        private void OnDie(Enemy enemy) =>
            SetDieState();

        public void SetIdleState() =>
            SwitchState<EnemyIdleState>();

        public void SetPatrolState() =>
            SwitchState<EnemyPatrolState>();

        public void SetAttackState() =>
            SwitchState<EnemyAttackState>();

        public void SetFollowState() =>
            SwitchState<EnemyFollowState>();

        private void SetDieState() =>
            SwitchState<EnemyDieState>();

        private void InitializeStates()
        {
            const int FirstState = 1;

            _states = new List<EnemyStateMachine>
            {
                new EnemyAttackState(this, this, _animator, _animationHasher, _enemyObserver, _attackBehaviour),
                new EnemyIdleState(this, this, _animator, _animationHasher, _enemyObserver, _enemyData),
                new EnemyPatrolState(this, this, _animator, _animationHasher, _enemyObserver, _enemyPatrolBehaviour),
                new EnemyFollowState(this, this, _animator, _animationHasher, _enemyObserver, _enemyFollowBehaviour),
                new EnemyDieState(this, this, _animator, _animationHasher, _enemyObserver, _capsuleCollider2D, _rigidbody2D, _shadowCaster2D),
            };

            _currentState = _states[FirstState];
        }

        public void SwitchState<T>() where T : EnemyStateMachine
        {
            var state = _states.FirstOrDefault(state => state is T);
            _currentState?.Stop();
            _currentState = state;
            _currentState?.Start();
        }
    }
}
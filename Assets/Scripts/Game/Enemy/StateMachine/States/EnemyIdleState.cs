using System.Collections;
using Game.Animation.AnimationHashes.Characters;
using Game.Enemy.EnemySettings.TestEnemy.Data.ScriptableObjects;
using Game.Enemy.Services;
using UnityEngine;

namespace Game.Enemy.StateMachine.States
{
    public class EnemyIdleState : EnemyStateMachine
    {
        private readonly EnemyObserver _enemyObserver;
        private readonly EnemyData _enemyData;
        private Coroutine _idleRoutine;
	
        public EnemyIdleState(EnemyBehaviour enemyBehaviour, IEnemyStateSwitcher stateSwitcher, Animator animator,
            AnimationHasher animationHasher, EnemyObserver enemyObserver, EnemyData enemyData)
            : base(enemyBehaviour, stateSwitcher, animator, animationHasher, enemyObserver)
        {
            _enemyData = enemyData;
            _enemyObserver = enemyObserver;
        }

        public override void Start()
        {
            Animator.Play(AnimationHasher.IdleHash);
            _enemyObserver.TouchedPlayer += OnSetAttackState;
            _enemyObserver.SeenPlayer += OnSetFollowState;
            StartIdleRoutine();
        }

        private void StartIdleRoutine()
        {
            StopIdleRoutine();
            _idleRoutine = EnemyBehaviour.StartCoroutine(IdleRoutine());
        }

        private IEnumerator IdleRoutine()
        {
            yield return new WaitForSeconds(_enemyData.IdleTime);
            StateSwitcher.SwitchState<EnemyPatrolState>();
        }

        private void OnSetAttackState(bool canTouch)
        {
            if (canTouch)
                EnemyBehaviour.SetAttackState();
        }

        private void OnSetFollowState(bool seenPlayer)
        {
            if (seenPlayer == true)
                EnemyBehaviour.SetFollowState();
        }

        private void StopIdleRoutine()
        {
            if (_idleRoutine != null)
                EnemyBehaviour.StopCoroutine(_idleRoutine);
        }
	
        public override void Stop()
        {
            StopIdleRoutine();	
            _enemyObserver.TouchedPlayer -= OnSetAttackState;
            _enemyObserver.SeenPlayer -= OnSetFollowState;
        }
    }
}
using Game.Animation.AnimationHashes.Characters;
using Game.Enemy.Services;
using Game.Enemy.StateMachine.Behaviours;
using UnityEngine;

namespace Game.Enemy.StateMachine.States
{
    public class EnemyAttackState : EnemyStateMachine
    {
        private readonly EnemyAttackBehaviour _attackBehaviour;

        public EnemyAttackState(EnemyBehaviour enemyBehaviour, IEnemyStateSwitcher stateSwitcher, Animator animator,
            AnimationHasher animationHasher, EnemyObserver enemyObserver, EnemyAttackBehaviour enemyAttackBehaviour) : base(
            enemyBehaviour, stateSwitcher, animator, animationHasher, enemyObserver)
        {
            _attackBehaviour = enemyAttackBehaviour;
        }

        public override void Start()
        {
            EnemyObserver.TouchedPlayer += SwitchState;
            _attackBehaviour.enabled = true;
        }

        private void SwitchState(bool isTouchPlayer)
        {
            if (isTouchPlayer == false)
                StateSwitcher.SwitchState<EnemyFollowState>();
        }

        public override void Stop()
        {
            EnemyObserver.TouchedPlayer -= SwitchState;
            _attackBehaviour.enabled = false;
        }
    }
}
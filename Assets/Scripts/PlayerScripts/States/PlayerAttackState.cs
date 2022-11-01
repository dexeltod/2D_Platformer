using System.Collections;
using UnityEngine;

namespace PlayerScripts.States
{
    public class PlayerAttackState : BaseState
    {
        private const int LayerIndex = 0;
        private IStateSwitcher _stateSwitcher;
        private readonly Weapon _weapon;
        private Coroutine _currentCoroutine;
        private bool _canAttack = true;

        public PlayerAttackState(Player player, IStateSwitcher stateSwitcher, AnimationHasher animationHasher,
            Animator animator, Weapon weapon) : base(player, stateSwitcher, animationHasher,
            animator)
        {
            _weapon = weapon;
        }

        public override void Start()
        {
            _currentCoroutine = Player.StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            Player.StartCoroutine(_weapon.AttackRoutine(Player.LookDirection));
            AnimatorStateInfo animatorInfo = Animator.GetCurrentAnimatorStateInfo(LayerIndex);
            float motionTime = animatorInfo.length;

            while (animatorInfo.shortNameHash != AnimationHasher.AttackHash)
            {
                animatorInfo = Animator.GetCurrentAnimatorStateInfo(LayerIndex);
                yield return null;
            }

            yield return new WaitForSeconds(motionTime);
            StateSwitcher.SwitchState<PlayerIdleState>();
        }

        public override void Stop()
        {
            if (_currentCoroutine == null)
                return;

            Player.StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
    }
}
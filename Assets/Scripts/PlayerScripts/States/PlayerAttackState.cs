using System.Collections;
using UnityEngine;

namespace PlayerScripts.States
{
    public class PlayerAttackState : BaseState
    {
        private const int LayerIndex = 0;
        private readonly Weapon _weapon;
        private IStateSwitcher _stateSwitcher;
        private Coroutine _currentCoroutine;
        private bool _canAttack = true;
        private Animator _animator;
        private AnimationHasher _animationHasher;

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

            while (_weapon.CanAttack == false)
            {
                yield return null;
            }

            AnimatorStateInfo animatorInfo = Animator.GetCurrentAnimatorStateInfo(LayerIndex);

            while (animatorInfo.shortNameHash != AnimationHasher.AttackHash)
            {
                animatorInfo = Animator.GetCurrentAnimatorStateInfo(LayerIndex);
                yield return null;
            }

            var waitForSeconds = new WaitForSeconds(animatorInfo.length);
            yield return waitForSeconds;
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
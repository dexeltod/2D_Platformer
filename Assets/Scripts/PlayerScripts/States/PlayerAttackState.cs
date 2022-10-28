using System.Collections;
using UnityEngine;

namespace PlayerScripts.States
{
    public class PlayerAttackState : BaseState
    {
        private const string LayerName = "Base Layer";
        private IStateSwitcher _stateSwitcher;
        private readonly Weapon _weapon;
        private Coroutine _currentCoroutine;

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
            const int LayerIndex = 0;

            _weapon.Attack(Player.LookDirection);
            AnimatorStateInfo animatorInfo = Animator.GetCurrentAnimatorStateInfo(LayerIndex);

            while (animatorInfo.shortNameHash != AnimationHasher.AttackHash)
            {
                animatorInfo = Animator.GetCurrentAnimatorStateInfo(LayerIndex);
                yield return null;
            }

            float motionTime = animatorInfo.length;

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
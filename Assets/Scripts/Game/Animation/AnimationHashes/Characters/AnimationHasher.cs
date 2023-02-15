using UnityEngine;

namespace Game.Animation.AnimationHashes.Characters
{
    public class AnimationHasher : MonoBehaviour
    {
        public int AttackSpeedHash { get; private set; }
        public int AttackHash { get; private set; }
        public int HandAttackRunHash { get; private set; }
        public int RacingHash { get; private set; }
        public int HandAttackHash { get; private set; }
        public int WalkHash { get; private set; }
        public int IdleHash { get; private set; }
        public int RunHash { get; private set; }
        public int FunHash { get; private set; }
        public int DieHash { get; private set; }
        public int JumpHash { get; private set; }
        public int FallHash { get; private set; }
        public int GlideHash { get; private set; }

        private void Awake()
        {
            WalkHash = Animator.StringToHash("isWalk");
            AttackHash = Animator.StringToHash("isAttack");
            IdleHash = Animator.StringToHash("isIdle");
            RunHash = Animator.StringToHash("isRun");
            RacingHash = Animator.StringToHash("isRacing");
            HandAttackHash = Animator.StringToHash("isHandAttack");
            AttackSpeedHash = Animator.StringToHash("attackSpeed");
            HandAttackRunHash = Animator.StringToHash("isHandAttackRun");
            FunHash = Animator.StringToHash("isFun");
            DieHash = Animator.StringToHash("isDie");
            JumpHash = Animator.StringToHash("isJump");
            FallHash = Animator.StringToHash("isFall");
            GlideHash = Animator.StringToHash("isGlide");
        }
    }
}
namespace Game.PlayerScripts.Weapons
{
    public sealed class Fist : WeaponTypes.MeleeAbstractWeapon
    {
        protected  override void Attack()
        {
            CanAttack = false;
            ChooseAnimation();
            PlayAttackAnimation(CurrentAnimationHash);

            var animation = Animator.GetCurrentAnimatorStateInfo(0);

            // yield return new WaitForSeconds(animation.speed);

            OnAnimationEnded();
            CanAttack = true;
        }

        private void ChooseAnimation()
        {
            CurrentAnimationHash = IsRun == false
                ? AnimationHasher.HandAttackHash
                : AnimationHasher.HandAttackRunHash;
        }
    }
}
namespace Game.PlayerScripts.Weapons
{
    public sealed class Fist : AbstractWeapon, IMeleeWeapon
    {
	    public override void GiveDamage(Enemy.Enemy target)
	    {
		    target.ApplyDamage(Damage);
	    }

	    protected  override void Attack()
	    {
		    if(CanAttack == false)
			    return;
		    
            CanAttack = false;
            var animation = Animator.GetCurrentAnimatorStateInfo(0);

            ChooseAnimation();
            StartCoroutine(PlayAnimationRoutine(CurrentAnimationHash));
            CanAttack = true;
        }

        private void ChooseAnimation()
        {
            CurrentAnimationHash = (IsRun == false)
                ? AnimationHasher.HandAttackHash
                : AnimationHasher.HandAttackRunHash;
        }
    }
}
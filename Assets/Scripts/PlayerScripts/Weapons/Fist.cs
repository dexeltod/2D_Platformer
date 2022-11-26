using System.Collections;
using PlayerScripts.Weapons;
using UnityEngine;

public class Fist : MeleeWeapon
{
	public override IEnumerator AttackRoutine(float direction)
	{
		CanAttack = false;
		ChooseAnimation();
		PlayAttackAnimation(CurrentAnimationHash);

		var animation = Animator.GetCurrentAnimatorStateInfo(0);
		
		yield return new WaitForSeconds(animation.speed * animation.speedMultiplier);
		
		OnAnimationEnded();
		CanAttack = true;
	}

	private void ChooseAnimation()
	{
		CurrentAnimationHash = _isRun == false
			? AnimationHasher.HandAttackHash
			: AnimationHasher.HandAttackRunHash;
	}
}
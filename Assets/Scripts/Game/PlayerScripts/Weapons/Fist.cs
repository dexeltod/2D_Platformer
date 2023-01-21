using System.Collections;
using PlayerScripts.Weapons;
using UnityEngine;

public class Fist : MeleeAbstractWeapon
{
	public override IEnumerator AttackRoutine(float direction)
	{
		CanAttack = false;
		ChooseAnimation();
		PlayAttackAnimation(CurrentAnimationHash);

		var animation = Animator.GetCurrentAnimatorStateInfo(0);
		
		yield return new WaitForSeconds(animation.speed);
		
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
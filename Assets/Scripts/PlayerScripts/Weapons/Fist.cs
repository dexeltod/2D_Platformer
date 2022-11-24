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
		
		yield return new WaitForSeconds(AttackSpeed);
		CanAttack = true;
	}

	private void ChooseAnimation()
	{
		CurrentAnimationHash = _isRun == false
			? AnimationHasher.HandAttackHash
			: AnimationHasher.HandAttackRunHash;
	}
}
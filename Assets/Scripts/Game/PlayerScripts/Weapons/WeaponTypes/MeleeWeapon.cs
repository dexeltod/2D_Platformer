using System.Collections;
using UnityEngine;

namespace PlayerScripts.Weapons
{
	public class MeleeWeapon : WeaponBase
	{
		[SerializeField] private int _maxCombo;

		private void OnEnable() => 
			MeleeWeaponTriggerInformant.Touched += GiveDamage;

		private void OnDisable() => 
			MeleeWeaponTriggerInformant.Touched -= GiveDamage;
		
		protected sealed override void PlayAttackAnimation(int animationHash) => 
			Animator.Play(animationHash);

		public override IEnumerator AttackRoutine(float direction)
		{
			CanAttack = false;
			PlayAttackAnimation(CurrentAnimationHash);
			yield return new WaitForSeconds(AttackSpeed);
			CanAttack = true;
		}

		public override void GiveDamage(Enemy target) =>
			target.ApplyDamage(Damage);
	}
}
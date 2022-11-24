using System.Collections;
using UnityEngine;

namespace PlayerScripts.Weapons
{
	public class MeleeWeapon : WeaponBase
	{
		[SerializeField] private MeleeWeaponTriggerInformant _meleeInformant;
		[SerializeField] private int _maxCombo;

		protected override void Awake()
		{
			base.Awake();
			_meleeInformant.Touched += GiveDamage;
		}

		protected sealed override void PlayAttackAnimation(int animationHash)
		{
			Animator.Play(animationHash);
		}

		private void OnDisable()
		{
			_meleeInformant.Touched -= GiveDamage;
		}

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
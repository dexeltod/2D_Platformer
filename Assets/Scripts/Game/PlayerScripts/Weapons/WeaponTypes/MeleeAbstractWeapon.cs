using UnityEngine;

namespace Game.PlayerScripts.Weapons.WeaponTypes
{
	public class MeleeAbstractWeapon : AbstractWeapon
	{
		[SerializeField] private int _maxCombo;

		private void OnEnable() => 
			MeleeWeaponTriggerInformant.Touched += GiveDamage;

		private void OnDisable() => 
			MeleeWeaponTriggerInformant.Touched -= GiveDamage;
		
		protected sealed override void PlayAttackAnimation(int animationHash) => 
			Animator.Play(animationHash);

		protected override void Attack()
		{
			CanAttack = false;
			PlayAttackAnimation(CurrentAnimationHash);
			// yield return new WaitForSeconds(AttackSpeed);
			CanAttack = true;
		}

		public override void GiveDamage(Enemy.Enemy target) =>
			target.ApplyDamage(Damage);
	}
}
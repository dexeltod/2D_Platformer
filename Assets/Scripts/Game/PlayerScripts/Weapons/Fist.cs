using Game.Enemy;
using UnityEngine;

namespace Game.PlayerScripts.Weapons
{
	public sealed class Fist : AbstractWeapon, IMeleeWeapon
	{
		public override void GiveDamage(IWeaponVisitor target)
		{
			target.FistVisit(this);
		}

		protected override void Attack()
		{
			if (CanAttack == false)
				return;

			WeaponAudio.clip = WeaponSound;

			WeaponAudio.Play();
			CanAttack = false;

			if (Animator != null)
			{
				var animation = Animator.GetCurrentAnimatorStateInfo(0);
			}

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
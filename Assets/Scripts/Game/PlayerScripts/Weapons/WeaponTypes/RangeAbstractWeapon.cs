using UnityEngine;

namespace Game.PlayerScripts.Weapons.WeaponTypes
{
	[RequireComponent(typeof(Bullets.BulletPool))]
	public class RangeAbstractWeapon : AbstractWeapon
	{
		[SerializeField] private float _bulletSpeed;
		[SerializeField] private Transform _bulletSpawnTransform;

		private Bullets.BulletPool _bulletPool;
		private Bullets.Bullet _currentBullet;

		protected override void OnAwake() =>
			_bulletPool = GetComponent<Bullets.BulletPool>();

		private void OnDisable()
		{
			if (_currentBullet != null)
				_currentBullet.IsTargetReached -= GiveDamage;
		}

		protected  override void Attack()
		{
			PlayAttackAnimation(CurrentAnimationHash);
			CanAttack = false;
			_currentBullet = _bulletPool.Get(_bulletSpawnTransform);
			// _currentBullet.SetSpeed(_bulletSpeed, direction);

			_currentBullet.IsTargetReached += GiveDamage;

			// yield return new WaitForSeconds(AttackSpeed);
			CanAttack = true;
		}

		public sealed override void GiveDamage(Enemy.Enemy target)
		{
			target.ApplyDamage(Damage);
			_currentBullet.IsTargetReached -= GiveDamage;
		}
	}
}
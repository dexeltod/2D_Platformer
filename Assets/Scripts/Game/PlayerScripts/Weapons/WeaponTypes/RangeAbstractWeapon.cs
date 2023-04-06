using Game.Enemy;
using Game.PlayerScripts.Weapons.Bullets;
using UnityEngine;

namespace Game.PlayerScripts.Weapons.WeaponTypes
{
	[RequireComponent(typeof(BulletPool))]
	public class RangeAbstractWeapon : AbstractWeapon
	{
		[SerializeField] private float _bulletSpeed;
		[SerializeField] private Transform _bulletSpawnTransform;

		private BulletPool _bulletPool;
		private Bullet _currentBullet;

		protected override void OnAwake() =>
			_bulletPool = GetComponent<BulletPool>();

		private void OnDisable()
		{
			if (_currentBullet != null)
				_currentBullet.IsTargetReached -= GiveDamage;
		}

		protected  override void Attack()
		{
			PlayAnimationRoutine(CurrentAnimationHash);
			CanAttack = false;
			_currentBullet = _bulletPool.Get(_bulletSpawnTransform);
			// _currentBullet.SetSpeed(_bulletSpeed, direction);

			_currentBullet.IsTargetReached += GiveDamage;

			// yield return new WaitForSeconds(AttackSpeed);
			CanAttack = true;
		}

		public sealed override void GiveDamage(IWeaponVisitor target)
		{
			target.RangeWeaponVisit(this);
			_currentBullet.IsTargetReached -= GiveDamage;
		}
	}
}
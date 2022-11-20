using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BulletPool))]
public class RangeWeapon : WeaponBase
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
	
	public override IEnumerator AttackRoutine(float direction)
	{
		SetAttackAnimation();
		CanAttack = false;
		_currentBullet = _bulletPool.Get(_bulletSpawnTransform);
		_currentBullet.SetSpeed(_bulletSpeed, direction);

		_currentBullet.IsTargetReached += GiveDamage;

		yield return new WaitForSeconds(AttackSpeed);
		CanAttack = true;
	}

	public sealed override void GiveDamage(Enemy target)
	{
		target.ApplyDamage(Damage);
		_currentBullet.IsTargetReached -= GiveDamage;
	}
}
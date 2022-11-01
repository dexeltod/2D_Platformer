using System;
using System.Collections;
using PlayerScripts.Weapons;
using UnityEngine;

public class ShotGun : Weapon, IRangedWeapon
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Transform _bulletSpawnTransform;

    private Bullet _currentBullet;
    public Transform BulletSpawnTransform => _bulletSpawnTransform;
    public float BulletSpeed => _bulletSpeed;

    public ShotGun()
    {
        Damage = 10;
        AttackSpeed = 0.5f;
    }
    
    private void OnDisable()
    {
        _bullet.IsTargetReached -= GiveDamage;
    }

    public override IEnumerator AttackRoutine(float direction)
    {
        _currentBullet = _bullet.Shoot(this, BulletSpawnTransform, direction);
        _currentBullet.IsTargetReached += GiveDamage;
        yield return new WaitForSeconds(AttackSpeed);
    }

    public override void GiveDamage(Enemy target)
    {
        target.ApplyDamage(Damage);
        _currentBullet.IsTargetReached -= GiveDamage;

    }
}
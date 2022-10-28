using System;
using PlayerScripts.Weapons;
using UnityEngine;

public class ShotGun : Weapon, IRangedWeapon
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Transform _bulletSpawnTransform;
    public Transform BulletSpawnTransform => _bulletSpawnTransform;
    public float BulletSpeed => _bulletSpeed;

    private void OnEnable()
    {
        _bullet.IsTargetReached += GiveDamage;
    }

    private void OnDisable()
    {
        _bullet.IsTargetReached -= GiveDamage;
    }

    public override void Attack(float direction)
    {
        _bullet.ShootBullet(this, BulletSpawnTransform, direction);
    }

    public override void GiveDamage(Enemy target)
    {
        target.ApplyDamage(Damage);
    }
}
using System.Collections;
using PlayerScripts.Weapons;
using UnityEngine;

public class Pistol : Weapon, IRangedWeapon
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _spawnBulletTransform;
    [SerializeField] private float _bulletSpeed;

    public Transform BulletSpawnTransform => _spawnBulletTransform;
    public float BulletSpeed => _bulletSpeed;

    private void OnEnable()
    {
        _bullet.IsTargetReached += GiveDamage;
    }

    private void OnDisable()
    {
        _bullet.IsTargetReached -= GiveDamage;
    }

    public override IEnumerator AttackRoutine(float direction)
    {
        _bullet = Instantiate(_bullet, _spawnBulletTransform.position, Quaternion.identity);
        yield return null;
    }

    public override void GiveDamage(Enemy target)
    {
        target.ApplyDamage(Damage);
    }
}
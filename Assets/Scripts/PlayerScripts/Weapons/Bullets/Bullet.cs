using System;
using PlayerScripts.Weapons;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public Action<Enemy> IsTargetReached;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void ShootBullet(IRangedWeapon weapon, Transform bulletTransform, float direction)
    {
        var bullet = Instantiate(this, bulletTransform.position, Quaternion.identity);
        bullet.SetBulletSpeed(direction * weapon.BulletSpeed);
    }

    private void SetBulletSpeed(float direction)
    {
        _rigidbody.velocity = new Vector2(direction, _rigidbody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D targetCollider)
    {
        if (targetCollider.TryGetComponent(out Enemy enemy) == true)
        {
            IsTargetReached?.Invoke(enemy);
        }

        Destroy(gameObject);
    }
}
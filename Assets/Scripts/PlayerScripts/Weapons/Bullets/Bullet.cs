using System;
using PlayerScripts.Weapons;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public event Action<Enemy> IsTargetReached;
    private Rigidbody2D _rigidbody;

    public Bullet(Action<Enemy> action)
    {
        IsTargetReached = action;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public Bullet Shoot(IRangedWeapon weapon, Transform bulletTransform, float direction)
    {
        var bullet = Instantiate(this, bulletTransform.position, Quaternion.identity);
        bullet.SetSpeed(direction * weapon.BulletSpeed);
        return bullet;
    }

    private void SetSpeed(float direction)
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
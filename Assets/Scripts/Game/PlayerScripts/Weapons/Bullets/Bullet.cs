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

    public void SetSpeed(float speed, float direction)
    {
        _rigidbody.velocity = new Vector2(speed * direction, _rigidbody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D targetCollider)
    {
        if (targetCollider.TryGetComponent(out Enemy enemy) == true)
        {
            IsTargetReached?.Invoke(enemy);
        }

        gameObject.SetActive(false);
    }
}
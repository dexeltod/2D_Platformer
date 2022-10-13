using UnityEngine;

[RequireComponent(typeof(EnemyObserve))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public abstract class EnemyBase : MonoBehaviour
{
    protected SpriteRenderer SpriteRenderer;
    protected EnemyObserve EnemyObserver;
    protected Animator Animator;

    private int _health = 30;
    private int _damage = 20;

    public EnemyBase(int health, int damage)
    {
        _health = health;
        _damage = damage;
    }
}

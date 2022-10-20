using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected SpriteRenderer SpriteRenderer;
    protected Animator Animator;

    private int _health = 30;
    private int _damage = 20;

    public EnemyBase(int health, int damage)
    {
        _health = health;
        _damage = damage;
    }
}

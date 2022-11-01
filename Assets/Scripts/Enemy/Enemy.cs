using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider2D), typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class Enemy : EnemyBase, IEnemy1Level
{
    [SerializeField] private Player _target;
    [SerializeField] private DataEnemy _enemyData;

    public event UnityAction WasDying;
    public event UnityAction WasHit;

    private int _maxHealth;
    private Coroutine _currentColorCoroutine;

    public Player Target => _target;
    public int Health { get; private set; }
    public int Damage { get; private set; }

    public Enemy(int health, int damage) : base(health, damage)
    {
    }

    private void Start()
    {
        Damage = _enemyData.Damage;
        _maxHealth = _enemyData.Health;
        Health = _maxHealth;
    }

    public void ApplyDamage(int damage)
    {
        if (Health <= 0)
        {
            WasDying?.Invoke();
            return;
        }

        WasHit?.Invoke();
        Health -= damage;
        ValidateHealth();
    }

    private void ValidateHealth()
    {
        const int MinHealthValue = 0;
        Health = Mathf.Clamp(Health, MinHealthValue, _maxHealth);
    }
}
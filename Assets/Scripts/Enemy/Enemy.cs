using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D), typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]

public class Enemy : EnemyBase, IEnemy1Level
{
    [SerializeField] private PlayerCharacter _target;
    [SerializeField] private DataEnemy _enemyData;

    public PlayerCharacter Target => _target;
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    public int Damage { get; private set; }

    public Enemy(int health, int damage) : base(health, damage)
    {
        Health = MaxHealth;
    }

    private void Start()
    {
        Damage = _enemyData.Damage;
        MaxHealth = _enemyData.Health;
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ApplyDamage(int damage)
    {
        if (Health <= 0)
            return;

        Health -= damage;
        ValidateHealth();
    }

    private void ValidateHealth()
    {
        int minHealthValue = 0;
        Health = Mathf.Clamp(Health, minHealthValue, MaxHealth);
    }
}
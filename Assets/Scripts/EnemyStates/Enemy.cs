using UnityEngine;

[RequireComponent(typeof(EnemyObserve))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class Enemy : EnemyBase, IEnemy1Level
{
    [SerializeField] private CharacterData _dataEntity;
    [SerializeField] private DataEnemyFight _dataEnemyFight;
    [SerializeField] private PlayerCharacter _target;

    public PlayerCharacter Target => _target;

    public int Health { get; private set; }
    public int Damage { get; private set; }

    public Enemy(int health, int damage) : base(health, damage)
    {
        Health = health;
        Damage = damage;
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        EnemyObserver = GetComponent<EnemyObserve>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ApplyDamage(int damage)
    {
        if (Health <= 0)
            return;

        Health -= damage;
    }

    public void Attack()
    {
        Debug.Log("Attack");
    }
}

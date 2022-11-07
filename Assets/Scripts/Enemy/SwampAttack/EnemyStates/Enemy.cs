using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class Enemy : MonoBehaviour, IEnemy1Level
{
    [SerializeField] private Player _target;
    [SerializeField] private DataEnemy _enemyData;

    private int _maxHealth;
    private Coroutine _currentColorCoroutine;
    private EnemyAttackBehaviour _attackBehaviour;

    public Player Target => _target;
    public int Health { get; private set; }
    public int Damage { get; private set; }

    public event UnityAction<Enemy> WasDying;
    public event UnityAction WasHit;

    private void Awake() =>
	    _attackBehaviour = GetComponent<EnemyAttackBehaviour>();

    private void Start()
    {
        Damage = _enemyData.Damage;
        _maxHealth = _enemyData.Health;
        Health = _maxHealth;
    }

    public void Initialize(PlayerHealth playerHealth) =>
	    _attackBehaviour.Initialize(playerHealth);

    public void ApplyDamage(int damage)
    {
        if (Health <= 0)
        {
            WasDying?.Invoke(this);
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
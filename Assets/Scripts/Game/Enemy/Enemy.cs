using Infrastructure;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class Enemy : MonoBehaviour
{
	[SerializeField] private EnemyData _enemyData;

	private Player _target;
	private EnemyBehaviour _enemyBehaviour;
	private Coroutine _currentColorCoroutine;
	private EnemyObserver _enemyObserver;

	private int _maxHealth;
	private IGameFactory _factory;

	public Player Target => _target;
	public int Health { get; private set; }
	public int Damage { get; private set; }

	public event UnityAction<Enemy> Dying;
	public event UnityAction WasHit;

	private void Awake()
	{
		_enemyBehaviour = GetComponent<EnemyBehaviour>();
	}

	private void Start()
	{
		_factory = ServiceLocator.Container.Single<IGameFactory>();
		_factory.MainCharacterCreated += OnLevelLoaded;
		Damage = _enemyData.Damage;
		_maxHealth = _enemyData.Health;
		Health = _maxHealth;
	}

	protected void StartFirstState() => 
		_enemyBehaviour.SwitchState<EnemyIdleState>();

	public void ApplyDamage(int damage)
	{
		if (Health <= 0)
		{
			Dying?.Invoke(this);
			return;
		}

		WasHit?.Invoke();
		Health -= damage;
		ValidateHealth();
	}

	private void OnLevelLoaded()
	{
		_target = _factory.MainCharacter.GetComponent<Player>();
		_factory.MainCharacterCreated -= OnLevelLoaded;
	}
	
	private void ValidateHealth()
	{
		const int MinHealthValue = 0;
		Health = Mathf.Clamp(Health, MinHealthValue, _maxHealth);
	}
}
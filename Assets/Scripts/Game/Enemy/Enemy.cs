using Game.CharactersSettingsSO.Characters.Enemy;
using Game.Enemy.Services;
using Game.Enemy.StateMachine;
using Game.Enemy.StateMachine.States;
using Game.PlayerScripts;
using Game.PlayerScripts.Weapons;
using Game.PlayerScripts.Weapons.WeaponTypes;
using Infrastructure.GameLoading;
using Infrastructure.Services.Factory;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemy
{
	//need decompose
    [RequireComponent(typeof(CapsuleCollider2D), typeof(EnemyBehaviour))]
    public abstract class Enemy : MonoBehaviour, IWeaponVisitor
    {
        [SerializeField] private EnemyData _enemyData;

        private Player _target;
        private EnemyBehaviour _enemyBehaviour;
        private Coroutine _currentColorCoroutine;
        private EnemyObserver _enemyObserver;

        private int _maxHealth;
        private IPlayerFactory _factory;
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
            _factory = ServiceLocator.Container.GetSingle<IPlayerFactory>();
            _factory.MainCharacterCreated += OnLevelLoaded;
            Damage = _enemyData.Damage;
            _maxHealth = _enemyData.Health;
            Health = _maxHealth;
        }

        protected void StartFirstState() => 
            _enemyBehaviour.SwitchState<EnemyIdleState>();

        public void FistVisit(Fist fist) => 
	        DefaultOverlapVisit(fist);

        public void RangeWeaponVisit(RangeAbstractWeapon shotGun)
        {
	        throw new System.NotImplementedException();
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

        private void DefaultOverlapVisit(AbstractWeapon weapon)
        {
	        weapon.PlayDamageSound();
	        
	        if (Health <= 0)
	        {
		        Dying?.Invoke(this);
		        return;
	        }

	        WasHit?.Invoke();
	        Health -= weapon.Damage;
	        ValidateHealth();
        }
    }
}
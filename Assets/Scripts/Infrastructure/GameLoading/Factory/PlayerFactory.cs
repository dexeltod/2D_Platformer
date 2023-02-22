using System;
using System.Threading.Tasks;
using Game.Animation.AnimationHashes.Characters;
using Game.PlayerScripts;
using Game.PlayerScripts.Move;
using Game.PlayerScripts.StateMachine;
using Game.PlayerScripts.StateMachine.States;
using Game.PlayerScripts.Weapons;
using Infrastructure.GameLoading.AssetManagement;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.GameLoading.Factory
{
    public class PlayerFactory : IPlayerFactory
    {
	    private const string Player = "Player";
	    
	    private readonly AnimationHasher _hasher;
        private readonly StateService _stateService;
        private readonly IAssetProvider _assetProvider;
        private readonly IInputService _inputService;

        private PlayerWeaponList _playerWeaponList;
        private WeaponFactory _weaponFactory;

        private PlayerMoney _playerMoney;
        private PhysicsMovement _physicsMovement;
        private Animator _animator;
        private AnimationHasher _animationHasher;
        private GroundChecker _groundChecker;
        private AnimatorFacade _animatorFacade;
        private PlayerStatesFactory _playerStatesFactory;

        public GameObject MainCharacter { get; private set; }

        public event Action MainCharacterCreated;

        public PlayerFactory(IAssetProvider assetProvider)
        {
            _inputService = ServiceLocator.Container.GetSingle<IInputService>();
            
            _stateService = new StateService();
            _assetProvider = assetProvider;
        }

        public async Task<GameObject> CreateHero(GameObject initialPoint)
        {
	        if (MainCharacter != null)
		        return MainCharacter;
	        
            MainCharacter = await _assetProvider.Instantiate(Player, initialPoint.transform.position);
            GetComponents();
            MainCharacterCreated?.Invoke();

            
            _playerWeaponList = new PlayerWeaponList(_weaponFactory, _playerMoney, MainCharacter.transform);
            CreatePlayerStateMachine();

            return MainCharacter;
        }

        private void GetComponents()
        {
            _weaponFactory = MainCharacter.GetComponent<WeaponFactory>();
            _physicsMovement = MainCharacter.GetComponent<PhysicsMovement>();
            _animator = MainCharacter.GetComponent<Animator>();
            _animationHasher = MainCharacter.GetComponent<AnimationHasher>();
            _groundChecker = MainCharacter.GetComponent<GroundChecker>();
            _playerMoney = MainCharacter.GetComponent<PlayerMoney>();
            _animatorFacade = MainCharacter.GetComponent<AnimatorFacade>();
        }

        private void CreatePlayerStateMachine()
        {
	        _playerStatesFactory = new PlayerStatesFactory(_groundChecker, _inputService, _animator, _animationHasher, _stateService, _animatorFacade,
	            _physicsMovement, _playerWeaponList);

            _playerStatesFactory.CreateTransitions();
            _playerStatesFactory.CreateStates();

            StateMachine stateMachine = new StateMachine(_stateService.Get<IdleState>());
        }
    }
}
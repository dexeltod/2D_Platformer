using System;
using System.Threading.Tasks;
using Game.Animation.AnimationHashes.Characters;
using Game.PlayerScripts;
using Game.PlayerScripts.Move;
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
			_assetProvider = assetProvider;
		}

		public async Task InstantiateHero(GameObject initialPoint) =>
			await CreateHeroGameObject(CreateDependencies, initialPoint);

		private async Task CreateHeroGameObject(Action onHeroInstantiated, GameObject initialPoint)
		{
			MainCharacter = await _assetProvider.Instantiate(Player, initialPoint.transform.position);
			onHeroInstantiated.Invoke();
		}

		private void CreateDependencies()
		{
			NullifyComponents();
			
			GetComponents();

			if (_playerWeaponList != null)
			{
				_playerWeaponList = null;
				GC.Collect();
			}

			_playerWeaponList = new PlayerWeaponList(_weaponFactory, _playerMoney, MainCharacter.transform);
			CreatePlayerStateMachine();
			MainCharacterCreated?.Invoke();
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

		private void NullifyComponents()
		{
			_weaponFactory = null;
			_physicsMovement = null;
			_animator = null;
			_animationHasher = null;
			_groundChecker = null;
			_playerMoney = null;
			_animatorFacade = null;
			
			GC.Collect();
		}

		private void CreatePlayerStateMachine()
		{
			if (_playerStatesFactory != null)
			{
				_playerStatesFactory = null;
				GC.Collect();
			}

			_playerStatesFactory = new PlayerStatesFactory(_groundChecker, _inputService, _animator, _animationHasher,
				_animatorFacade,
				_physicsMovement, _playerWeaponList);

			_playerStatesFactory.CreateTransitions();
			_playerStatesFactory.CreateStates();
			_playerStatesFactory.CreateStateMachineAndSetState();
		}
	}
}
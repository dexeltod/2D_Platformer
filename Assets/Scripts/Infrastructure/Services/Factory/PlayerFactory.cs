using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Animation.AnimationHashes.Characters;
using Game.PlayerScripts;
using Game.PlayerScripts.Move;
using Game.PlayerScripts.Weapons;
using Infrastructure.Data.PersistentProgress;
using Infrastructure.Data.Serializable;
using Infrastructure.GameLoading;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Interfaces;
using UnityEngine;
using View.UI_Scripts.Shop;

namespace Infrastructure.Services.Factory
{
	public class PlayerFactory : IPlayerFactory
	{
		private const string Player = "Player";

		private readonly AnimationHasher _hasher;
		private readonly IAssetProvider _assetProvider;
		private readonly IPersistentProgressService _progressService;
		private readonly IInputService _inputService;
		private readonly GameProgress _progress;

		private PlayerWeaponList _playerWeaponList;
		private WeaponFactory _weaponFactory;

		private PlayerMoney _playerMoney;
		private PhysicsMovement _physicsMovement;
		private Animator _animator;
		private AnimationHasher _animationHasher;
		private GroundChecker _groundChecker;
		private AnimatorFacade _animatorFacade;
		private PlayerStatesFactory _playerStatesFactory;
		private WallCheckTrigger _wallCheckTrigger;

		public GameObject MainCharacter { get; private set; }

		public event Action MainCharacterCreated;

		public PlayerFactory(IAssetProvider assetProvider, IPersistentProgressService progressService)
		{
			_inputService = ServiceLocator.Container.GetSingle<IInputService>();
			_assetProvider = assetProvider;
			_progress = progressService.GameProgress;
		}

		public async UniTask InstantiateHero(GameObject initialPoint)
		{
			await CreateHeroGameObject(initialPoint);
			await CreateDependenciesAsync();
		}

		private async UniTask CreateHeroGameObject(GameObject initialPoint)
		{
			MainCharacter = await _assetProvider.Instantiate(Player, initialPoint.transform.position);
		}

		private async UniTask CreateDependenciesAsync()
		{
			if (_playerWeaponList != null)
			{
				_playerWeaponList = null;
				GC.Collect();
			}

			List<ItemScriptableObject> items = await GetItems();

			NullifyComponents();

			GetComponents();

			_playerWeaponList = new PlayerWeaponList(_weaponFactory, _playerMoney, MainCharacter.transform, items);
			CreatePlayerStateMachine();
			MainCharacterCreated?.Invoke();
		}

		private async UniTask<List<ItemScriptableObject>> GetItems()
		{
			string[] items = _progress.PlayerProgressData.SerializableItemsData.GetItemReferences();

			List<ItemScriptableObject> itemScriptableObjects = new();

			foreach (var item in items)
			{
				itemScriptableObjects.Add(await _assetProvider.LoadAsyncByGUID<ItemScriptableObject>(item));
			}

			return itemScriptableObjects;
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

			_wallCheckTrigger = MainCharacter.GetComponentInChildren<WallCheckTrigger>();
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
			_wallCheckTrigger = null;

			GC.Collect();
		}

		private void CreatePlayerStateMachine()
		{
			if (_playerStatesFactory != null)
			{
				_playerStatesFactory = null;
				GC.Collect();
			}

			_playerStatesFactory = new PlayerStatesFactory(_groundChecker, _wallCheckTrigger, _inputService, _animator,
				_animationHasher,
				_animatorFacade,
				_physicsMovement, _playerWeaponList);

			_playerStatesFactory.CreateTransitions();
			_playerStatesFactory.CreateStates();
			_playerStatesFactory.CreateStateMachineAndSetState();
		}
	}
}
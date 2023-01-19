using System;
using Infrastructure.AssetProvide;
using Infrastructure.Services;
using PlayerScripts.TestStateMachine;
using UnityEngine;

namespace Infrastructure
{
	public class PlayerFactory : IPlayerFactory
	{
		private readonly AnimationHasher _hasher;
		private readonly StateService _stateService;
		private readonly IAssetProvider _assetProvider;
		private readonly IInputService _inputService;

		private PhysicsMovement _physicsMovement;
		private Animator _animator;
		private AnimationHasher _animationHasher;

		public GameObject MainCharacter { get; private set; }

		public event Action MainCharacterCreated;

		public PlayerFactory(IAssetProvider assetProvider)
		{
			_inputService = ServiceLocator.Container.Single<IInputService>();
			_stateService = new StateService();
			_assetProvider = assetProvider;
		}

		public GameObject CreateHero(GameObject initialPoint)
		{
			MainCharacter = _assetProvider.Instantiate(ConstantNames.PlayerPrefabPath, initialPoint.transform.position);
			MainCharacterCreated.Invoke();

			GetComponents();
			CreatePlayerStateMachine();

			return MainCharacter;
		}

		private void CreatePlayerStateMachine()
		{
			PlayerStatesFactory playerStatesFactory =
				new PlayerStatesFactory(_inputService, _animator, _animationHasher, _stateService, _physicsMovement);
			playerStatesFactory.CreateTransitions();
			playerStatesFactory.CreateStates();
			TestStateMachine stateMachine = new TestStateMachine(_stateService.Get<IdleState>());
		}

		private void GetComponents()
		{
			_physicsMovement = MainCharacter.GetComponent<PhysicsMovement>();
			_animator = MainCharacter.GetComponent<Animator>();
			_animationHasher = MainCharacter.GetComponent<AnimationHasher>();
		}
	}
}
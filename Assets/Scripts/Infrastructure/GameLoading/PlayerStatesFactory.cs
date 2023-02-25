using System;
using Game.Animation.AnimationHashes.Characters;
using Game.PlayerScripts;
using Game.PlayerScripts.Move;
using Game.PlayerScripts.StateMachine;
using Game.PlayerScripts.StateMachine.States;
using Game.PlayerScripts.StateMachine.Transitions;
using Game.PlayerScripts.StateMachine.Transitions.AttackTo;
using Game.PlayerScripts.Weapons;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.GameLoading
{
	public class PlayerStatesFactory
	{
		private readonly GroundChecker _groundChecker;
		private readonly IInputService _inputService;
		private readonly Animator _animator;
		private readonly StateService _stateService;
		private readonly AnimatorFacade _animatorFacade;
		private readonly PhysicsMovement _physicsMovement;
		private readonly PlayerWeaponList _playerWeaponList;
		private readonly PlayerSceneSwitcher _playerSceneSwitcher;
		private readonly AbstractWeapon _abstractWeapon;
		private readonly AnimationHasher _animationHasher;

		private IStateTransition _anyToAttackTransition;
		private IStateTransition _anyToRunTransition;
		private IStateTransition _anyToDeadTransition;
		private IStateTransition _anyToIdleTransition;
		private IStateTransition _anyToJumpTransition;
		private IStateTransition _anyToFallTransition;
		
		private IStateTransition _attackToRunTransition;
		private IStateTransition _attackToIdleTransition;
		private IStateTransition _attackToFallTransition;
		
		private IStateTransition _anyToChangeSceneTransitions;

		public PlayerStatesFactory(GroundChecker groundChecker, IInputService inputService, Animator animator,
			AnimationHasher hasher, AnimatorFacade animatorFacade, PhysicsMovement physicsMovement,
			PlayerWeaponList playerWeaponList, PlayerSceneSwitcher playerSceneSwitcher)
		{
			_groundChecker = groundChecker;
			_inputService = inputService;
			_animator = animator;
			_animationHasher = hasher;
			_animatorFacade = animatorFacade;
			_physicsMovement = physicsMovement;
			_playerWeaponList = playerWeaponList;
			_playerSceneSwitcher = playerSceneSwitcher;

			_stateService = new StateService();
			_abstractWeapon = _playerWeaponList.GetEquippedWeapon();
		}

		
		public void CreateStateMachineAndSetState()
		{
			new StateMachine(_stateService.Get<IdleState>());
		}

		public void CreateTransitions()
		{
			_anyToIdleTransition =
				new AnyToIdleTransition(_stateService, _inputService, _physicsMovement, _groundChecker);
			_anyToRunTransition =
				new AnyToRunTransition(_stateService, _inputService, _physicsMovement, _groundChecker);
			_anyToFallTransition = new AnyToFallTransition(_stateService, _physicsMovement);
			_anyToJumpTransition = new AnyToJumpTransition(_stateService, _inputService, _groundChecker);
			_anyToAttackTransition = new AnyToAttackTransition(_stateService, _inputService, _groundChecker);
			_anyToDeadTransition = new AnyToDeadTransition(_stateService);

			_attackToRunTransition = new AttackToRunTransition(_stateService, _abstractWeapon, _physicsMovement);
			_attackToIdleTransition = new AttackToIdleTransition(_stateService, _abstractWeapon, _physicsMovement);
			_attackToFallTransition = new AttackToFallTransition(_stateService, _abstractWeapon, _physicsMovement);
			_anyToChangeSceneTransitions = new AnyToChangeSceneTransition(_stateService, _playerSceneSwitcher);
		}

		public void CreateStates()
		{
			CreateIdleState();
			CreateRunState();
			CreateFallState();
			CreateJumpState();
			CreateAttackState();
			CreateDeadState();
			CreateChangeSceneState();
		}

		private void CreateChangeSceneState()
		{
			_stateService.Register(new ChangeSceneState(
				_inputService, _animator, _animationHasher, transitions: new IStateTransition[] { }
			));
		}

		private void CreateIdleState()
		{
			_stateService.Register(
				new IdleState(
					_inputService, _physicsMovement, _animator, _animationHasher, _animatorFacade,
					new[]
					{
						_anyToDeadTransition,
						_anyToRunTransition,
						_anyToAttackTransition,
						_anyToJumpTransition,
						_anyToFallTransition,
						_anyToChangeSceneTransitions
					}
				)
			);
		}

		private void CreateRunState()
		{
			_stateService.Register(new RunState(_inputService, _animator, _physicsMovement, _animationHasher,
					_animatorFacade,
					transitions: new[]
					{
						_anyToChangeSceneTransitions,
						_anyToDeadTransition,
						_anyToAttackTransition,
						_anyToIdleTransition,
						_anyToJumpTransition,
						_anyToFallTransition,
					}
				)
			);
		}

		private void CreateFallState()
		{
			_stateService.Register(new FallState(_inputService, _animator, _animationHasher, _animatorFacade,
				transitions: new[]
				{
					_anyToChangeSceneTransitions,
					_anyToIdleTransition,
					_anyToRunTransition,
				},
				_physicsMovement));
		}

		private void CreateJumpState()
		{
			_stateService.Register(new JumpState(_physicsMovement, _inputService, _animator, _animationHasher,
				_animatorFacade,
				transitions: new[]
				{
					_anyToChangeSceneTransitions,
					_anyToIdleTransition,
					_anyToRunTransition,
					_anyToFallTransition,
				}));
		}

		private void CreateAttackState()
		{
			_stateService.Register(
				new AttackState(
					_inputService, _playerWeaponList, _abstractWeapon, _animator, _animationHasher, _physicsMovement,
					transitions: new[]
					{
						_anyToChangeSceneTransitions,
						_anyToDeadTransition,
						_attackToFallTransition,
						_attackToIdleTransition,
						_attackToRunTransition,
					}));
		}

		private void CreateDeadState()
		{
			_stateService.Register(
				new DeadState(
					_inputService, _animator, _animationHasher,
					transitions: new IStateTransition[] { }
				)
			);
		}

		
	}
}
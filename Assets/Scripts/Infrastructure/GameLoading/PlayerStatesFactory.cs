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
		private readonly PhysicsMovement _physicsMovement;
		private readonly PlayerWeaponList _playerWeaponList;
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

		public PlayerStatesFactory(GroundChecker groundChecker, IInputService inputService, Animator animator,
			AnimationHasher hasher,
			StateService stateService, PhysicsMovement physicsMovement, PlayerWeaponList playerWeaponList)
		{
			_groundChecker = groundChecker;
			_inputService = inputService;
			_animator = animator;
			_animationHasher = hasher;
			_stateService = stateService;
			_physicsMovement = physicsMovement;
			_playerWeaponList = playerWeaponList;

			_abstractWeapon = _playerWeaponList.GetEquippedWeapon();
		}

		public void CreateStates()
		{
			CreateAttackState();
			CreateIdleState();
			CreateRunState();
			CreateFallState();
			CreateDeadState();
			CreateJumpState();
		}

		public void CreateTransitions()
		{
			_anyToIdleTransition = new AnyToIdleTransition(_stateService, _inputService, _physicsMovement, _groundChecker);
			_anyToRunTransition = new AnyToRunTransition(_stateService, _inputService, _physicsMovement, _groundChecker);
			_anyToFallTransition = new AnyToFallTransition(_stateService, _physicsMovement, _groundChecker);
			_anyToJumpTransition = new AnyToJumpTransition(_stateService, _inputService, _groundChecker);
			_anyToAttackTransition = new AnyToAttackTransition(_stateService, _inputService, _groundChecker);
			_anyToDeadTransition = new AnyToDeadTransition(_stateService);

			_attackToRunTransition = new AttackToRunTransition(_stateService, _abstractWeapon, _physicsMovement);
			_attackToIdleTransition = new AttackToIdleTransition(_stateService, _abstractWeapon, _physicsMovement);
			_attackToFallTransition = new AttackToFallTransition(_stateService, _abstractWeapon, _physicsMovement);
		}

		private void CreateIdleState() =>
			_stateService.Register(
				new IdleState(
					_inputService, _physicsMovement, _animator, _animationHasher,
					new[]
					{
						_anyToDeadTransition,
						_anyToRunTransition,
						_anyToAttackTransition,
						_anyToJumpTransition,
						_anyToFallTransition,
					}));

		private void CreateRunState() =>
			_stateService.Register(
				new RunState(
					_inputService, _physicsMovement, _animator, _animationHasher,
					transitions: new[]
					{
						_anyToDeadTransition,
						_anyToAttackTransition,
						_anyToIdleTransition,
						_anyToJumpTransition,
						_anyToFallTransition,
					}));

		private void CreateFallState()
		{
			_stateService.Register(new FallState(_inputService, _animator, _animationHasher,
				transitions: new[]
				{
					_anyToIdleTransition,
					_anyToRunTransition,
				},
				_physicsMovement));
		}

		private void CreateJumpState()
		{
			_stateService.Register(new JumpState(_physicsMovement, _inputService, _animator, _animationHasher,
				transitions: new[]
				{
					_anyToFallTransition,
				}));
		}

		private void CreateAttackState() =>
			_stateService.Register(
				new AttackState(
					_inputService, _playerWeaponList, _abstractWeapon, _animator, _animationHasher, _physicsMovement,
					transitions: new[]
					{
						_anyToDeadTransition,
						_attackToFallTransition,
						_attackToIdleTransition,
						_attackToRunTransition,
					}));

		private void CreateDeadState() =>
			_stateService.Register(
				new DeadState(
					_inputService, _animator, _animationHasher,
					transitions: new IStateTransition[] { }
				)
			);
	}
}
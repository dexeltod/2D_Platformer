using Infrastructure.Services;
using PlayerScripts;
using PlayerScripts.TestStateMachine;
using PlayerScripts.Weapons;
using UnityEngine;

public class PlayerStatesFactory
{
	private readonly GroundChecker _groundChecker;
	private readonly IInputService _inputService;
	private readonly Animator _animator;
	private readonly StateService _stateService;
	private readonly PhysicsMovement _physicsMovement;
	private readonly PlayerWeapon _playerWeapon;
	private readonly AbstractWeapon _abstractWeapon;
	private readonly AnimationHasher _animationHasher;

	private IStateTransition _anyToAttackTransition;
	private IStateTransition _anyToRunTransition;
	private IStateTransition _anyToDeadTransition;
	private IStateTransition _anyToIdleTransition;
	private IStateTransition _anyToJumpTransition;
	private IStateTransition _anyToFallTransition;

	public PlayerStatesFactory(GroundChecker groundChecker ,IInputService inputService, Animator animator, AnimationHasher hasher,
		StateService stateService, PhysicsMovement physicsMovement, PlayerWeapon playerWeapon,
		AbstractWeapon weaponBase)
	{
		_groundChecker = groundChecker;
		_inputService = inputService;
		_animator = animator;
		_animationHasher = hasher;
		_stateService = stateService;
		_physicsMovement = physicsMovement;
		_playerWeapon = playerWeapon;
		_abstractWeapon = weaponBase;
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
				new[]
				{
					_anyToDeadTransition,
					_anyToAttackTransition,
					_anyToIdleTransition,
					_anyToJumpTransition,
					_anyToFallTransition,
				}));

	private void CreateDeadState() =>
		_stateService.Register(
			new DeadState(
				_inputService, _animator, _animationHasher,
				new IStateTransition[] { }
			)
		);

	private void CreateFallState()
	{
		_stateService.Register(new FallState(_inputService, _animator, _animationHasher,
			new[]
			{
				_anyToIdleTransition,
				_anyToRunTransition,
			}, 
			_physicsMovement));
	}

	private void CreateJumpState()
	{
		_stateService.Register(new JumpState(_physicsMovement, _inputService, _animator, _animationHasher,
			new[]
			{
				_anyToFallTransition,
			}));
	}

	private void CreateAttackState() =>
		_stateService.Register(
			new AttackState(
				_inputService, _playerWeapon, _abstractWeapon, _animator, _animationHasher, _physicsMovement,
				new[]
				{
					_anyToRunTransition,
					_anyToDeadTransition,
					_anyToIdleTransition,
				}));
}
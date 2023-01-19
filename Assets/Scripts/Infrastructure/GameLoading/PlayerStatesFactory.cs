using Infrastructure.Services;
using PlayerScripts.TestStateMachine;
using UnityEngine;

public class PlayerStatesFactory
{
	private readonly IInputService _inputService;
	private readonly Animator _animator;
	private readonly StateService _stateService;
	private readonly PhysicsMovement _physicsMovement;
	private readonly AnimationHasher _animationHasher;

	private ITestTransition _anyToAttackTransition;
	private ITestTransition _anyToRunTransition;
	private ITestTransition _anyToDeadTransition;
	private ITestTransition _anyToIdleTransition;

	public PlayerStatesFactory(IInputService inputService, Animator animator, AnimationHasher hasher,
		StateService stateService, PhysicsMovement physicsMovement)
	{
		_inputService = inputService;
		_animator = animator;
		_animationHasher = hasher;
		_stateService = stateService;
		_physicsMovement = physicsMovement;
	}

	public void CreateStates()
	{
		CreateAttackState();
		CreateIdleState();
		CreateRunState();
		CreateDeadState();
	}

	public void CreateTransitions()
	{
		_anyToIdleTransition = new AnyToIdleTransition(_stateService, _inputService);
		_anyToAttackTransition = new AnyToAttackTransition(_stateService, _inputService);
		_anyToRunTransition = new AnyToRunTransition(_stateService, _inputService);
		_anyToDeadTransition = new AnyToDeadTransition(_stateService);
	}

	private void CreateIdleState() =>
		_stateService.Register(
			new IdleState(
				_inputService, _animator, _animationHasher,
				new[]
				{
					_anyToDeadTransition,
					_anyToRunTransition,
					_anyToAttackTransition,
				}
			)
		);

	private void CreateRunState() =>
		_stateService.Register(
			new RunState(
				_inputService, _animator, _animationHasher,
				new[]
				{
					_anyToDeadTransition,
					_anyToAttackTransition,
					_anyToIdleTransition
				}
			)
		);

	private void CreateDeadState() =>
		_stateService.Register(
			new DeadState(
				_inputService, _animator, _animationHasher,
				new ITestTransition[] {}
			)
		);

	private void CreateAttackState() =>
		_stateService.Register(
			new AttackState(
				_inputService, _animator, _animationHasher, _physicsMovement,
				new[]
				{
					_anyToRunTransition,
					_anyToDeadTransition,
					_anyToIdleTransition
				}
			)
		);
}
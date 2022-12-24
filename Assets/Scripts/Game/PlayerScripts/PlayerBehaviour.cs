using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using Infrastructure.Services;
using PlayerScripts;
using PlayerScripts.States;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AnimationHasher))]
[RequireComponent(typeof(Player), typeof(WeaponFactory))]
[RequireComponent(typeof(PlayerWeapon))]
public class PlayerBehaviour : MonoBehaviour, IPlayerStateSwitcher
{
	private Player _player;

	private PlayerWeapon _playerWeapon;
	private PhysicsMovement _physicsMovement;
	private AnimationHasher _animationHasher;
	private Animator _animator;
	private IInputService _inputService;

	private List<PlayerStateMachine> _states = new();
	private PlayerStateMachine _currentState;

	private void Awake()
	{
		_inputService = ServiceLocator.Container.Single<IInputService>();
		_physicsMovement = GetComponent<PhysicsMovement>();
		_playerWeapon = GetComponent<PlayerWeapon>();
		_player = GetComponent<Player>();
		_animationHasher = GetComponent<AnimationHasher>();
		_animator = GetComponent<Animator>();
		InitializeStates();
	}

	private void OnEnable()
	{
		_physicsMovement.Glided += SetGlideState;
		_inputService.AttackButtonUsed += SetAttackState;
		_inputService.JumpButtonUsed += SetJumpState;
	}

	private void OnDisable()
	{
		_physicsMovement.Glided -= SetGlideState;
		_inputService.AttackButtonUsed -= SetAttackState;
		_inputService.JumpButtonUsed -= SetJumpState;
	}

	public void SetIdleState()
	{
		if (_physicsMovement.MovementDirection == Vector2.zero)
			SwitchState<PlayerIdleState>();
	}

	public void SetRunState(float direction) =>
		SwitchState<PlayerRunState>();

	private void SetAttackState()
	{
		SwitchState<PlayerAttackState>();
	}

	private void SetGlideState()
	{
		SwitchState<PlayerGlideState>();
	}

	private void SetJumpState()
	{
		SwitchState<PlayerJumpState>();
	}

	public void SwitchState<T>() where T : PlayerStateMachine
	{
		var state = _states.FirstOrDefault(state => state is T);
		_currentState?.Stop();
		state?.Start();
		_currentState = state;
	}

	private void InitializeStates()
	{
		_states = new List<PlayerStateMachine>
		{
			new PlayerIdleState(_player, this, _animationHasher, _animator, _physicsMovement, _inputService),
			new PlayerRunState(_player, this, _animationHasher, _animator, _inputService, _physicsMovement),
			new PlayerJumpState(_player, this, _animationHasher, _animator, _physicsMovement),
			new PlayerFallState(_player, this, _animationHasher, _animator, _physicsMovement),
			new PlayerGlideState(_player, this, _animationHasher, _animator, _physicsMovement),
			new PlayerAttackState(_player, this, _animationHasher, _animator, _playerWeapon, _physicsMovement),
		};

		_currentState = _states[0];
	}
}
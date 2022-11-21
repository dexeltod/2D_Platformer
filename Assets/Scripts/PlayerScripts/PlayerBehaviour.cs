using System;
using System.Collections.Generic;
using System.Linq;
using PlayerScripts;
using PlayerScripts.States;
using UnityEngine;

[RequireComponent(typeof(InputSystemReader), typeof(Animator),
	typeof(AnimationHasher))]
[RequireComponent(typeof(Player), typeof(WeaponFactory), typeof(WeaponFactory))]
public class PlayerBehaviour : MonoBehaviour, IStateSwitcher
{
	private Player _player;

	private PlayerWeapon _playerWeapon;
	private PhysicsMovement _physicsMovement;
	private AnimationHasher _animationHasher;
	private Animator _animator;
	private InputSystemReader _inputSystemReader;

	private BaseState _currentState;
	private List<BaseState> _states = new();

	private void Awake()
	{
		_inputSystemReader = GetComponent<InputSystemReader>();
		_physicsMovement = GetComponent<PhysicsMovement>();
		_playerWeapon = GetComponent<PlayerWeapon>();
		_player = GetComponent<Player>();
		_animationHasher = GetComponent<AnimationHasher>();
		_animator = GetComponent<Animator>();
		InitializeStates();
	}

	private void OnEnable()
	{
		_physicsMovement.Fallen += SetFallState;
		_physicsMovement.Glided += SetGlideState;
		_inputSystemReader.AttackButtonPerformed += SetAttackState;
		_inputSystemReader.JumpButtonUsed += SetJumpState;
	}

	private void OnDisable()
	{
		_physicsMovement.Fallen -= SetFallState;
		_physicsMovement.Glided -= SetGlideState;
		_inputSystemReader.AttackButtonPerformed -= SetAttackState;
		_inputSystemReader.JumpButtonUsed -= SetJumpState;
	}

	public void SetFallState()
	{
		if (_physicsMovement.MovementDirection.y < 0)
			SwitchState<PlayerFallState>();
	}

	public void SetIdleState()
	{
		if (_physicsMovement.MovementDirection == Vector2.zero)
			SwitchState<PlayerIdleState>();
	}

	public void SetRunState(float direction) =>
		SwitchState<PlayerRunState>();

	public void SetAttackState() =>
		SwitchState<PlayerAttackState>();

	public void SetGlideState()
	{
		SwitchState<PlayerGlideState>();
	}

	public void SetJumpState()
	{
		SwitchState<PlayerJumpState>();
	}

	public void SwitchState<T>() where T : BaseState
	{
		var state = _states.FirstOrDefault(state => state is T);
		_currentState?.Stop();
		state?.Start();
		_currentState = state;
	}

	private void InitializeStates()
	{
		_states = new List<BaseState>()
		{
			new PlayerIdleState(_player, this, _animationHasher, _animator, _physicsMovement, _inputSystemReader),
			new PlayerAttackState(_player, this, _animationHasher, _animator, _playerWeapon, _physicsMovement),
			new PlayerRunState(_player, this, _animationHasher, _animator),
			new PlayerJumpState(_player, this, _animationHasher, _animator, _physicsMovement),
			new PlayerFallState(_player, this, _animationHasher, _animator, _physicsMovement),
			new PlayerGlideState(_player, this, _animationHasher, _animator, _physicsMovement),
		};

		_currentState = _states[0];
	}
}
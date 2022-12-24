using Infrastructure.Services;
using PlayerScripts;
using PlayerScripts.States;
using UnityEngine;

public class PlayerIdleState : PlayerStateMachine
{
	private readonly IPlayerStateSwitcher _stateSwitcher;
	private readonly PhysicsMovement _physicsMovement;
	private readonly IInputService _inputSystemReaderService;

	public PlayerIdleState(Player player, IPlayerStateSwitcher stateSwitcher, AnimationHasher animationHasher,
		Animator animator, PhysicsMovement physicsMovement, IInputService inputSystemReaderService) : base(player,
		stateSwitcher, animationHasher, animator)
	{
		_inputSystemReaderService = inputSystemReaderService;
		_physicsMovement = physicsMovement;
		_stateSwitcher = stateSwitcher;
	}

	public override void Start()
	{
		_inputSystemReaderService.VerticalButtonUsed += SetRunState;
		_physicsMovement.Jumped += SetJumpState;
		Animator.Play(AnimationHasher.IdleHash);
	}

	private void SetRunState(float direction) =>
		_stateSwitcher.SwitchState<PlayerRunState>();

	private void SetJumpState() =>
		_stateSwitcher.SwitchState<PlayerJumpState>();

	public override void Stop()
	{
		_inputSystemReaderService.VerticalButtonUsed -= SetRunState;
		_physicsMovement.Jumped -= SetJumpState;
	}
}
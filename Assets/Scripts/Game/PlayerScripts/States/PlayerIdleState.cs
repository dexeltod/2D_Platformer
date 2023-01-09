using Infrastructure.Services;
using PlayerScripts;
using PlayerScripts.States;
using UnityEngine;

public class PlayerIdleState : PlayerStateMachine
{
	private readonly PhysicsMovement _physicsMovement;
	private readonly IInputService _inputSystemReaderService;

	public PlayerIdleState(Player player, IPlayerStateSwitcher stateSwitcher, AnimationHasher animationHasher,
		Animator animator, PhysicsMovement physicsMovement, IInputService inputSystemReaderService) : base(player,
		stateSwitcher, animationHasher, animator)
	{
		_inputSystemReaderService = inputSystemReaderService;
		_physicsMovement = physicsMovement;
	}

	public override void Start()
	{
		_inputSystemReaderService.VerticalButtonUsed += SetRunState;
		_physicsMovement.Jumped += SetJumpState;
		Animator.Play(AnimationHasher.IdleHash);
		Debug.Log(AnimationHasher.IdleHash);
	}

	private void SetRunState(float direction)
	{
		Animator.Play(AnimationHasher.RunHash);
		StateSwitcher.SwitchState<PlayerRunState>();
	}

	private void SetJumpState() =>
		StateSwitcher.SwitchState<PlayerJumpState>();

	public override void Stop()
	{
		Animator.StopPlayback();
		_inputSystemReaderService.VerticalButtonUsed -= SetRunState;
		_physicsMovement.Jumped -= SetJumpState;
	}
}
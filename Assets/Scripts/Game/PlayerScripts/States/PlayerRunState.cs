using Infrastructure.Services;
using PlayerScripts;
using PlayerScripts.States;
using UnityEngine;

public class PlayerRunState : PlayerStateMachine
{
	private readonly IInputService _inputSystemReaderService;
	private readonly PhysicsMovement _physicsMovement;

	public PlayerRunState(Player player, IPlayerStateSwitcher stateSwitcher, AnimationHasher animationHasher,
		Animator animator, IInputService inputSystemReaderService, PhysicsMovement physicsMovement)
		: base(player, stateSwitcher, animationHasher, animator)
	{
		_physicsMovement = physicsMovement;
		_inputSystemReaderService = inputSystemReaderService;
	}

	public override void Start()
	{
		Animator.Play(AnimationHasher.RunHash);
		Debug.Log("runState");
		_inputSystemReaderService.VerticalButtonCanceled += SetIdleState;
		_physicsMovement.Fallen += SetFallState;

	}

	private void SetIdleState() => 
		StateSwitcher.SwitchState<PlayerIdleState>();

	private void SetFallState() => 
		StateSwitcher.SwitchState<PlayerFallState>();

	public override void Stop()
	{
		_inputSystemReaderService.VerticalButtonCanceled -= SetIdleState;
		_physicsMovement.Fallen -= SetFallState;
	}
}
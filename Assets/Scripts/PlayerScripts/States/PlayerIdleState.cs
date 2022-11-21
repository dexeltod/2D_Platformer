using PlayerScripts;
using PlayerScripts.States;
using UnityEngine;

public class PlayerIdleState : BaseState
{
	private readonly IStateSwitcher _stateSwitcher;
	private readonly PhysicsMovement _physicsMovement;
	private readonly InputSystemReader _inputSystemReader;

	public PlayerIdleState(Player player, IStateSwitcher stateSwitcher, AnimationHasher animationHasher,
		Animator animator, PhysicsMovement physicsMovement, InputSystemReader inputSystemReader) : base(player,
		stateSwitcher, animationHasher, animator)
	{
		_inputSystemReader = inputSystemReader;
		_physicsMovement = physicsMovement;
		_stateSwitcher = stateSwitcher;
	}

	public override void Start()
	{
		_inputSystemReader.VerticalMoveButtonUsed += SetRunState;
		_physicsMovement.Jumped += SetJumpState;
		Animator.Play(AnimationHasher.IdleHash);
	}

	private void SetRunState(float direction) =>
		_stateSwitcher.SwitchState<PlayerRunState>();

	private void SetJumpState() =>
		_stateSwitcher.SwitchState<PlayerJumpState>();

	public override void Stop()
	{
		_inputSystemReader.VerticalMoveButtonUsed -= SetRunState;
		_physicsMovement.Jumped -= SetJumpState;
	}
}
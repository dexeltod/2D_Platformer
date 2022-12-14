using PlayerScripts;
using PlayerScripts.States;
using UnityEngine;

public class PlayerRunState : PlayerStateMachine
{
	private readonly InputSystemReader _inputSystemReader;
	private readonly PhysicsMovement _physicsMovement;

	public PlayerRunState(Player player, IPlayerStateSwitcher stateSwitcher, AnimationHasher animationHasher,
		Animator animator, InputSystemReader inputSystemReader, PhysicsMovement physicsMovement)
		: base(player, stateSwitcher, animationHasher, animator)
	{
		_physicsMovement = physicsMovement;
		_inputSystemReader = inputSystemReader;
	}

	public override void Start()
	{
		Animator.Play(AnimationHasher.RunHash);
		_inputSystemReader.VerticalMoveButtonCanceled += SetIdleState;
		_physicsMovement.Fallen += SetFallState;

	}

	private void SetIdleState() => 
		StateSwitcher.SwitchState<PlayerIdleState>();

	private void SetFallState() => 
		StateSwitcher.SwitchState<PlayerFallState>();

	public override void Stop()
	{
		_inputSystemReader.VerticalMoveButtonCanceled -= SetIdleState;
		_physicsMovement.Fallen -= SetFallState;
	}
}
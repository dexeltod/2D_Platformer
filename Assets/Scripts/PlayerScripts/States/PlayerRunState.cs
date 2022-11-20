using PlayerScripts;
using UnityEngine;

public class PlayerRunState : BaseState
{
	private PlayerInputListener _inputListener;
	private float _moveDirection;
	private IStateSwitcher _stateSwitcher;

	public PlayerRunState(Player player, IStateSwitcher stateSwitcher, AnimationHasher animationHasher,
		Animator animator)
		: base(player, stateSwitcher, animationHasher, animator)
	{
		_moveDirection = player.LookDirection;
	}

	public override void Start()
	{
		Animator.Play(AnimationHasher.RunHash);
	}

	public override void Stop()
	{
	}
}
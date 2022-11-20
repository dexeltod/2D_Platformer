using PlayerScripts;
using UnityEngine;

public class PlayerIdleState : BaseState
{
	private IStateSwitcher _stateSwitcher;
	
    public PlayerIdleState(Player player, IStateSwitcher stateSwitcher, AnimationHasher animationHasher,
        Animator animator) : base(player, stateSwitcher, animationHasher, animator)
    {
	    _stateSwitcher = stateSwitcher;
    }

    public override void Start()
    {
        Animator.Play(AnimationHasher.IdleHash);
    }

    public override void Stop()
    {
    }
}
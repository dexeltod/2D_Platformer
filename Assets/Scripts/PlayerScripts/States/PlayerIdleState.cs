using PlayerScripts;
using UnityEngine;

public class PlayerIdleState : BaseState
{
    public PlayerIdleState(Player player, IStateSwitcher stateSwitcher, AnimationHasher animationHasher,
        Animator animator) : base(player, stateSwitcher, animationHasher, animator)
    {
    }

    public override void Start()
    {
        Animator.CrossFade(AnimationHasher.IdleHash, 0.1f);
    }

    public override void Stop()
    {
    }
}
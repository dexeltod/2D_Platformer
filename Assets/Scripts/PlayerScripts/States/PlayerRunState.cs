using PlayerScripts;
using UnityEngine;

public class PlayerRunState : BaseState
{
    private PlayerMoveController _moveController;
    private float _moveDirection;

    public PlayerRunState(Player player, IStateSwitcher stateSwitcher, AnimationHasher animationHasher, Animator animator) : base(player, stateSwitcher, animationHasher, animator)
    {
        _moveDirection = player.LookDirection;
    }

    public override void Start()
    {
        Animator.CrossFade(AnimationHasher.RunHash, 0);
    }

    public override void Stop()
    {
        throw new System.NotImplementedException();
    }
}
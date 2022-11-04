using System;
using UnityEngine;

public class MoveToAttackTransition : Transition
{
    [SerializeField] private MoveToPlayerBehaviour _moveBehaviour;
    public override void Enable()
    {
        _moveBehaviour.PlayerReached += ChangeState;
    }

    private void OnDisable()
    {
        _moveBehaviour.PlayerReached -= ChangeState;
    }

    private void ChangeState()
    {
        IsNeedTransition = true;
    }
        
}

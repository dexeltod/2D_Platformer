using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MoveToAttackTransition : Transition
{
    [SerializeField] private MoveToPlayerBehaviour _moveBehaviour;
    public override void Enable()
    {
        _moveBehaviour.enabled = true;
        _moveBehaviour.PlayerReached += ChangeState;
    }

    private void OnDisable()
    {
        _moveBehaviour.enabled = false;
        _moveBehaviour.PlayerReached -= ChangeState;
    }

    private void ChangeState()
    {
        IsNeedTransition = true;
    }
        
}

using UnityEngine;

public class AttackToWinTransition : Transition
{
    [SerializeField] private AttackPlayerBehaviour _attackBehaviour;

    public override void Enable()
    {
        _attackBehaviour.PlayerDied += ChangeState;
    }

    private void OnDisable()
    {
        _attackBehaviour.PlayerDied -= ChangeState;
    }

    private void ChangeState()
    {
        IsNeedTransition = true;
    }
}
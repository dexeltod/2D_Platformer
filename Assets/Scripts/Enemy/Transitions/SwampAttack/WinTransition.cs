using UnityEngine;

public class WinTransition : Transition
{
    [SerializeField] private EnemyAttackBehaviour _enemyAttackBehaviour;
    
    public override void Enable()
    {
        _enemyAttackBehaviour.PlayerDied += ChangeState;
    }

    private void OnDisable()
    {
        _enemyAttackBehaviour.PlayerDied -= ChangeState;
    }

    private void ChangeState()
    {
        IsNeedTransition = true;
    }
}

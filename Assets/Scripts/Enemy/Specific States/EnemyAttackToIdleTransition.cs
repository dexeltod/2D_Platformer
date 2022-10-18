using UnityEngine;
public class EnemyAttackToIdleTransition : Transition
{
    [SerializeField] private EnemyObserve _enemyLook;
    [SerializeField] private D_EntityVisibility _entityVisibility;

    public override void Enable()
    {
    }

    private void Update()
    {
        if (_enemyLook.DistanceBetweenEnemy >= _entityVisibility.VisibilityRange && _enemyLook.AngleFacingDirection <= _enemyLook.AngleFacingDirection)
        {
            IsNeedTransition = true;
        }
    }
}

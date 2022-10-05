using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPatrolToIdleTransition : Transition
{
    [SerializeField] private UnityEvent _ledgeNotLooked;
    [SerializeField] private D_EntityVisibility _entityVisibility;
    [SerializeField] private EnemyLookAround _enemyLook;

    public override void Enable(){}

    private void Update()
    {
        CheckEnemy();
        ChangeStateToIdle();
    }

    private void CheckEnemy()
    {
        if (_enemyLook.IsSeeEnemy())
        {
            IsNeedTransition = true;
        }
    }

    private void ChangeStateToIdle()
    {
        if (!_enemyLook.CheckLedge() || _enemyLook.CheckColliderHorizontal())
        {
            IsNeedTransition = true;
            _ledgeNotLooked?.Invoke();
        }
    }
}

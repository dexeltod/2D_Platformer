using UnityEngine;
using UnityEngine.Events;

public class EnemyPatrolToIdleTransition : Transition
{
    [SerializeField] private EnemyObserve _observer;
    [SerializeField] private UnityEvent _ledgeNotCollide;

    public override void Enable(){}

    private void Update()
    {
        CheckEnemy();
        ChangeStateToIdle();
    }

    private void CheckEnemy()
    {
        if (_observer.IsSeeEnemy() == true)
            IsNeedTransition = true;
    }

    private void ChangeStateToIdle()
    {
        bool isNoWay = _observer.CheckLedge() == false || _observer.CheckColliderHorizontal() == true;

        if (isNoWay)
        {
            IsNeedTransition = true;
            _ledgeNotCollide?.Invoke();
        }
    }
}

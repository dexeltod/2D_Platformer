using UnityEngine;
using UnityEngine.Events;

public class PatrolState : State
{
    [SerializeField] private CharacterData _dataEntity;
    [SerializeField] private EnemyPatrolBehaviour _patrolBehaviour;

    private Animator _animator;
    private string _animBoolName = "isWalk";

    private void Awake()
    {
        _animator = GetComponentInParent<Animator>();
    }

    private void OnEnable()
    {
        SetMovementState(true);
    }

    private void OnDisable()
    {
        SetMovementState(false);
    }

    private void SetMovementState(bool workState)
    {
        _patrolBehaviour.enabled = workState;
        _animator.SetBool(_animBoolName, workState);
    }
}

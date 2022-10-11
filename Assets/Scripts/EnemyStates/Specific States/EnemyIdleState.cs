using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Animator _animator;
    private string _animBoolName = "isIdle";

    private void Awake()
    {
        _animator = GetComponentInParent<Animator>();
    }

    private void OnEnable()
    {
        SetBoolName(true);
    }

    private void OnDisable()
    {
        SetBoolName(false);
    }

    private void SetBoolName(bool workState)
    {
        _animator.SetBool(_animBoolName, workState);
    }
}

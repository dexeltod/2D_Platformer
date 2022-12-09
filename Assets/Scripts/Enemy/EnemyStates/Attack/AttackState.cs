using UnityEngine;

public class AttackState : State
{
    [SerializeField] private EnemyAttackBehaviour _attackBehaviour;
    
    private void OnEnable()
    {
        _attackBehaviour.enabled = true;
    }
    
    private void OnDisable()
    {
        _attackBehaviour.enabled = false;
    }
}
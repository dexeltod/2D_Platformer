using UnityEngine;

public class AttackToMoveTransition : Transition
{
    [SerializeField] private EnemyAttackBehaviour _attackBehaviour;
    
    public override void Enable() => 
	    _attackBehaviour.PlayerOutOfRangeAttack += ChangeState;

    public  void OnDisable() => 
	    _attackBehaviour.PlayerOutOfRangeAttack -= ChangeState;

    private void ChangeState() => 
	    IsNeedTransition = true;
}
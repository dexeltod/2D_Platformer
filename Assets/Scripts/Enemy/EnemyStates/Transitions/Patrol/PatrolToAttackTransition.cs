using UnityEngine;

public class PatrolToAttackTransition : Transition
{
	[SerializeField] private EnemyPatrolBehaviour _enemyPatrolBehaviour;

	public override void Enable() => 
		_enemyPatrolBehaviour.NeedAttack += SwitchState;

	private void OnDisable() => 
		_enemyPatrolBehaviour.NeedAttack -= SwitchState;

	private void SwitchState() =>
		IsNeedTransition = true;
	
}
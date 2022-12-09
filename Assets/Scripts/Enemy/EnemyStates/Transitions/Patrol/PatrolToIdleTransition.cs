using UnityEngine;

public class PatrolToIdleTransition : Transition
{
	[SerializeField] private EnemyPatrolBehaviour _enemyPatrolBehaviour;

	public override void Enable() =>
		_enemyPatrolBehaviour.NoWay += SwitchState;

	private void SwitchState() =>
		IsNeedTransition = true;

	private void OnDisable() =>
		_enemyPatrolBehaviour.NoWay -= SwitchState;
}
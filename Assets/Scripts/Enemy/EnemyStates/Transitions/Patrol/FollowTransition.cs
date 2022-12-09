using UnityEngine;

public class FollowTransition : Transition
{
	[SerializeField] private EnemyObserver _enemyPlayerChecker;

	public override void Enable() =>
		_enemyPlayerChecker.SeenEnemy += SwitchState;

	private void OnDisable() =>
		_enemyPlayerChecker.SeenEnemy -= SwitchState;

	private void SwitchState() => 
		IsNeedTransition = true;
}
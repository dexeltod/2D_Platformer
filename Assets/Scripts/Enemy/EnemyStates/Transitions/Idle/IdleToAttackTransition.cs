using UnityEngine;

public class IdleToAttackTransition : Transition
{
	[SerializeField] private EnemyMeleeRangeInformer _enemyMeleeRangeInformer;

	public override void Enable() =>
		_enemyMeleeRangeInformer.TouchedPlayer += ChangeState;

	private void OnDisable() =>
		_enemyMeleeRangeInformer.TouchedPlayer -= ChangeState;

	private void ChangeState(bool isAttack)
	{
		if (isAttack == true)
		{
			_enemyMeleeRangeInformer.TouchedPlayer -= ChangeState;
			IsNeedTransition = true;
		}
	}
}
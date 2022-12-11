using UnityEngine;

public class FollowToAttackTransition : Transition
{
	[SerializeField] private EnemyFollowForPlayerBehaviour _moveBehaviour;

	public override void Enable() =>
		_moveBehaviour.PlayerReached += ChangeState;

	private void OnDisable() =>
		_moveBehaviour.PlayerReached -= ChangeState;

	private void ChangeState(bool isReached)
	{
		if (isReached == true)
			IsNeedTransition = true;
	}
}
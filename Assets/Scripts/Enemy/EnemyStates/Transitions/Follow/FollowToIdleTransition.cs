using UnityEngine;

public class FollowToIdleTransition : Transition
{
	[SerializeField] private EnemyFollowForPlayerBehaviour _enemyFollowForPlayer;
	
	public override void Enable()
	{
		_enemyFollowForPlayer.PlayerReached += SwitchState;
	}

	private void SwitchState(bool canReachPlayer)
	{
		
	}
}
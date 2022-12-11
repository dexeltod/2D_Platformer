using UnityEngine;

public class FollowPlayerState : State
{
	[SerializeField] private EnemyFollowForPlayerBehaviour _enemyFollowForPlayerBehaviour;

	private void OnEnable() =>
		_enemyFollowForPlayerBehaviour.enabled = true;

	private void OnDisable() =>
		_enemyFollowForPlayerBehaviour.enabled = false;
}
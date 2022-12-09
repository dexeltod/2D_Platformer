using UnityEngine;

public class FollowPlayerState : State
{
	[SerializeField] private FollowPlayerBehaviour _followPlayerBehaviour;

	private void OnEnable() =>
		_followPlayerBehaviour.enabled = true;

	private void OnDisable() =>
		_followPlayerBehaviour.enabled = false;
}
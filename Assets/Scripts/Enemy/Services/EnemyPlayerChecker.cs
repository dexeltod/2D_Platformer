using System;
using UnityEngine;

public class EnemyPlayerChecker : MonoBehaviour
{
	[SerializeField] private Transform _eyePosition;
	[SerializeField] private ContactFilter2D _playerLayer;

	public event Action<bool> SeenPlayer;

	private void OnTriggerEnter2D(Collider2D targetCollider)
	{
		if (targetCollider.TryGetComponent(out Player target))
		{
			Vector2 playerPosition = target.transform.position;

			bool isHitPlayer = Physics2D.Raycast(_eyePosition.position, playerPosition, _playerLayer.layerMask);
			
			if(isHitPlayer) 
				SeenPlayer.Invoke(true);
		}
	}

	private void OnTriggerExit2D(Collider2D targetCollider)
	{
		if(targetCollider.TryGetComponent(out Player _))
			SeenPlayer.Invoke(false);
	}
}
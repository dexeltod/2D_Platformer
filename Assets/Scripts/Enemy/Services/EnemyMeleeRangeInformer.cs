using System;
using UnityEngine;

public class EnemyMeleeRangeInformer : MonoBehaviour
{
	public event Action<bool> TouchedPlayer;
	
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.TryGetComponent(out Player _)) 
			TouchedPlayer.Invoke(true);
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.TryGetComponent(out Player _)) 
			TouchedPlayer.Invoke(false);
	}
}

using System;
using UnityEngine;

public class EnemyMeleeRangeInformer : MonoBehaviour
{
	public event Action<bool> TouchedPlayer;
	
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.TryGetComponent(out Player _))
		{
			Debug.Log("can attack");
			TouchedPlayer.Invoke(true);
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.TryGetComponent(out Player _))
		{
			Debug.Log("cant attack");
			TouchedPlayer.Invoke(false);
		}
	}
}

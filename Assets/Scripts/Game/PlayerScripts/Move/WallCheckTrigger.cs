using System;
using UnityEngine;

public class WallCheckTrigger : MonoBehaviour
{
	[SerializeField] private LayerMask _groundLayer;
	private const string Ground = "Ground";

	public bool IsWallTouched { get; private set; }
	public event Action<bool> WallTouched;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (LayerMask.LayerToName(col.gameObject.layer) != Ground)
			return;
		IsWallTouched = true;
		WallTouched?.Invoke(true);
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (LayerMask.LayerToName(col.gameObject.layer) != Ground)
			return;

		IsWallTouched = false;
		WallTouched?.Invoke(false);
	}
}
using System;
using SuperTiled2Unity.Scripts;
using UnityEngine;

namespace Game.PlayerScripts.Move
{
	[RequireComponent(typeof(Collider2D))]
	public class WallCheckTrigger : MonoBehaviour
	{
		public bool IsWallTouched { get; private set; }
		public event Action<bool> WallTouched;

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (!col.gameObject.TryGetComponent(out SuperColliderComponent _))
				return;

			IsWallTouched = true;
			WallTouched?.Invoke(true);
		}

		private void OnTriggerExit2D(Collider2D col)
		{
			if (!col.gameObject.TryGetComponent(out SuperColliderComponent _))
				return;

			IsWallTouched = false;
			WallTouched?.Invoke(false);
		}
	}
}
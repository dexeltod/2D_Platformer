using System;
using Game.PlayerScripts;
using UnityEngine;

namespace Game.Enemy.Services
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class EnemyMeleeDamageTrigger : MonoBehaviour
	{
		private Collider2D _collider;
	
		public event Action<bool> TouchedPlayer;

		private void Awake()
		{
			_collider = GetComponent<BoxCollider2D>();
		}

		private void OnEnable() => 
			_collider.enabled = true;

		private void OnDisable()
		{
			_collider.transform.position = Vector2.zero;
			_collider.enabled = false;
		}

		private void OnTriggerEnter2D(Collider2D touchedCollider)
		{
			if (touchedCollider.TryGetComponent(out Player _))
				TouchedPlayer?.Invoke(true);
		}
	}
}
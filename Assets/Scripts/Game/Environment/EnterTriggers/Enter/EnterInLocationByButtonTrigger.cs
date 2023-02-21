using System;
using Game.PlayerScripts;
using UnityEngine;

namespace Game.Environment.EnterTriggers.Enter
{
	public class EnterInLocationByButtonTrigger : MonoBehaviour
	{
		public event Action InTriggerEntered;
		
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.TryGetComponent(out Player _))
			{
				InTriggerEntered?.Invoke();
			}
		}
	}
}

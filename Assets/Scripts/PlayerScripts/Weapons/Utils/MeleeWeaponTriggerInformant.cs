using System;
using UnityEngine;

public class MeleeWeaponTriggerInformant : MonoBehaviour
{
	public event Action<Enemy> Touched;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent(out Enemy enemy)) 
			Touched.Invoke(enemy);
	}
}
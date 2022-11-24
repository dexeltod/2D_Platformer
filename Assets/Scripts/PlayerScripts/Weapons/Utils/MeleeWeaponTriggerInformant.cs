using UnityEngine;
using UnityEngine.Events;

public class MeleeWeaponTriggerInformant : MonoBehaviour
{
	private int _currentAngleRotation;
	public event UnityAction<Enemy> Touched;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent(out Enemy enemy))
		{
			Touched?.Invoke(enemy);
		}
	}
}
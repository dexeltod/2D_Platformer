using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class FeetDustEnabler : MonoBehaviour
{
	[SerializeField] private PhysicsMovement _physicsMovement;
	[SerializeField] private Transform _feetPosition;

	private VisualEffect _visualEffect;

	private void Awake() =>
		_visualEffect = GetComponent<VisualEffect>();

	private void OnEnable() =>
		_physicsMovement.Running += ChangeEffectEnabling;

	private void OnDisable() =>
		_physicsMovement.Running -= ChangeEffectEnabling;

	private void ChangeEffectEnabling(bool isWork)
	{
		if (isWork == false)
			return;
		
		var position = _feetPosition.position;

		_visualEffect.transform.position = new Vector3(position.x, position.y, 0);
		_visualEffect.Play();
	}
}
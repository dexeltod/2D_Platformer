using UnityEngine;

public class MaterialSetter : MonoBehaviour
{
	[SerializeField] private PhysicsMaterial2D _new;
	private PhysicsMaterial2D _current;

	private void Start()
	{
		_current = GetComponent<PhysicsMaterial2D>();
		_current = _new;
	}
}

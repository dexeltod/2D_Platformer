using PlayerScripts.TestStateMachine;
using PlayerScripts.Weapons;
using UI_Scripts.Shop;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerBehaviour), typeof(PhysicsMovement))]
public class Player : MonoBehaviour
{
	private PhysicsMovement _physicsMovement;
	private float _lookDirection = -1;
	public float LookDirection => _lookDirection;

	private StateService _stateService;

	public event UnityAction<int, AbstractWeapon> Bought;

	private void Awake()
	{
		_physicsMovement = GetComponent<PhysicsMovement>();
	}

	private void OnEnable()
	{
		_physicsMovement.Rotating += SetLookDirection;
	}

	public void TryBuyWeapon(AbstractWeapon weaponBase, ItemInfo itemInfo) =>
		Bought?.Invoke(itemInfo.Price, weaponBase);

	private void OnDisable()
	{
		_physicsMovement.Rotating -= SetLookDirection;
	}

	private void SetLookDirection(bool direction)
	{
		int rotateDirection = direction ? 0 : 180;
		transform.rotation = Quaternion.Euler(0, rotateDirection, 0);
	}
}
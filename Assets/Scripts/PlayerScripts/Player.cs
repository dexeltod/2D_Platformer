using UI_Scripts.Shop;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InputSystemReader), typeof(PlayerBehaviour))]
public class Player : MonoBehaviour
{
	private PhysicsMovement _physicsMovement;
	private SpriteRenderer _spriteRenderer;

	private float _lookDirection = -1;
	public float LookDirection => _lookDirection;

	public event UnityAction<int, WeaponBase> Bought;

	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_physicsMovement = GetComponent<PhysicsMovement>();
	}

	private void OnEnable()
	{
		_physicsMovement.Rotated += SetLookDirection;
	}

	public void TryBuyWeapon(WeaponBase weaponBase, ItemInfo itemInfo) =>
		Bought?.Invoke(itemInfo.Price, weaponBase);

	private void OnDisable()
	{
		_physicsMovement.Rotated -= SetLookDirection;
	}

	private void SetLookDirection(float direction)
	{
		bool isRotated = direction == -1;
		_spriteRenderer.flipX = isRotated;
	}
}
using UI_Scripts.Shop;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InputSystemReader), typeof(PlayerBehaviour))]
public class Player : MonoBehaviour
{
	private PhysicsMovement _physicsMovement;
	private InputSystemReader _inputSystemReader;
	private PlayerBehaviour _playerBehaviour;
	private float _lookDirection = -1;
	public float LookDirection => _lookDirection;

	public event UnityAction<int, WeaponBase> Bought;

	private void Awake()
	{
		_inputSystemReader = GetComponent<InputSystemReader>();
		_playerBehaviour = GetComponent<PlayerBehaviour>();
		_physicsMovement = GetComponent<PhysicsMovement>();
	}

	private void OnEnable()
	{
		_inputSystemReader.VerticalMoveButtonUsed += SetLookDirection;
		_physicsMovement.Glided += _playerBehaviour.SetGlideState;
		_inputSystemReader.AttackButtonPerformed += _playerBehaviour.SetAttackState;
		_inputSystemReader.VerticalMoveButtonUsed += _playerBehaviour.SetRunState;
		_inputSystemReader.JumpButtonUsed += _playerBehaviour.SetJumpState;
	}

	public void TryBuyWeapon(WeaponBase weaponBase, ItemInfo itemInfo) =>
		Bought?.Invoke(itemInfo.Price, weaponBase);

	private void OnDisable()
	{
		_inputSystemReader.VerticalMoveButtonUsed -= SetLookDirection;
		_physicsMovement.Glided -= _playerBehaviour.SetGlideState;
		_inputSystemReader.AttackButtonPerformed -= _playerBehaviour.SetAttackState;
		_inputSystemReader.VerticalMoveButtonUsed -= _playerBehaviour.SetRunState;
		_inputSystemReader.JumpButtonUsed -= _playerBehaviour.SetJumpState;
	}

	private void SetLookDirection(float direction) =>
		_lookDirection = direction;
}
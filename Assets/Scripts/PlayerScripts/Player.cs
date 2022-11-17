using UI_Scripts.Shop;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InputSystemReader), typeof(PlayerBehaviour))]
public class Player : MonoBehaviour
{
	public event UnityAction<int, WeaponBase> Bought;

	private InputSystemReader _inputSystemReader;
	private PlayerBehaviour _playerBehaviour;
	private float _lookDirection = -1;
	public float LookDirection => _lookDirection;

	private void Awake()
	{
		_inputSystemReader = GetComponent<InputSystemReader>();
		_playerBehaviour = GetComponent<PlayerBehaviour>();
	}

	private void OnEnable()
	{
		_inputSystemReader.VerticalMoveButtonUsed += SetLookDirection;
		_inputSystemReader.AttackButtonPerformed += _playerBehaviour.SetAttackState;
	}

	public void TryBuyWeapon(WeaponBase weaponBase, ItemInfo itemInfo) =>
		Bought?.Invoke(itemInfo.Price, weaponBase);

	private void OnDisable()
	{
		_inputSystemReader.VerticalMoveButtonUsed -= SetLookDirection;
		_inputSystemReader.AttackButtonPerformed -= _playerBehaviour.SetAttackState;
	}

	private void SetLookDirection(float direction) =>
		_lookDirection = direction;
}
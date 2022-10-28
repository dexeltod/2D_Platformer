using UnityEngine;

[RequireComponent(typeof(InputSystemReader), typeof(PlayerBehaviour))]
public class Player : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    private InputSystemReader _inputSystemReader;
    private PlayerBehaviour _playerBehaviour;
    private float _lookDirection = -1;

    public float LookDirection => _lookDirection;
    public Weapon Weapon => _weapon;

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

    private void OnDestroy()
    {
        _inputSystemReader.VerticalMoveButtonUsed -= SetLookDirection;
        _inputSystemReader.AttackButtonPerformed -= _playerBehaviour.SetAttackState;
    }

    private void SetLookDirection(float direction)
    {
        _lookDirection = direction;
    }
}
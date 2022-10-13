using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputSystemReader : MonoBehaviour
{
    public UnityAction InteractButtonUsed;
    public UnityAction<float> VerticalMoveButtonUsed;
    public UnityAction<float> JumpButtonUsed;
    public UnityAction<float> AttackButtonUsed;

    [SerializeField] private PlayerMoveController _playerMoveContoller;
    [SerializeField] private PlayerAttack _playerAttack;   

    private float _buttonAttackValue;
    private float _buttonJumpValue;
    private InputSystem _inputActions;
    private Vector2 _buttonMoveValue;

    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            InteractButtonUsed?.Invoke();
        }
    }

    private void Awake()
    {
        _inputActions = new InputSystem();

        _inputActions.Player.Move.performed += value => OnHorizontalMovement(value);
        _inputActions.Player.Jump.performed += value => OnJump(value);
        _inputActions.Player.Attack.performed += value => OnAttack(value);
        _inputActions.Player.Use.started += value => OnUse(value);
    }

    private void OnEnable() => _inputActions.Enable();

    private void OnDisable() => _inputActions.Disable();    

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _buttonAttackValue = context.ReadValue<float>();
            AttackButtonUsed?.Invoke(_buttonAttackValue);
        }
    }

    private void OnHorizontalMovement(InputAction.CallbackContext context)
    {
        _buttonMoveValue = context.ReadValue<Vector2>();
        VerticalMoveButtonUsed?.Invoke(_buttonMoveValue.x);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        _buttonJumpValue = context.ReadValue<float>();
        JumpButtonUsed?.Invoke(_buttonJumpValue);
    }
}

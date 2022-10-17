using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputSystemReader : MonoBehaviour
{
    public UnityAction InteractButtonUsed;
    public UnityAction<float> VerticalMoveButtonUsed;
    public UnityAction<float> JumpButtonUsed;
    public UnityAction<float> AttackButtonUsed;

    private float _buttonAttackValue;
    private float _buttonJumpValue;
    private InputSystem _inputActions;
    private Vector2 _buttonMoveValue;

    private void Awake()
    {
        _inputActions = new InputSystem();

        _inputActions.Player.Move.performed += OnHorizontalMovement;
        _inputActions.Player.Jump.performed += OnJump;
        _inputActions.Player.Attack.performed += OnAttack;
        _inputActions.Player.Use.started += OnUse;
    }

    private void OnUse(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            InteractButtonUsed?.Invoke();
        }
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
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputSystemReader : MonoBehaviour
{
    public UnityAction InteractButtonUsed;
    public UnityAction AttackButtonPerformed;

    public UnityAction<float> VerticalMoveButtonUsed;
    public UnityAction<Vector2> JumpButtonUsed;

    private float _buttonAttackValue;
    private Vector2 _buttonJumpValue;
    private InputSystem _inputActions;

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
            InteractButtonUsed?.Invoke();
    }

    private void OnEnable() => _inputActions.Enable();

    private void OnDisable() => _inputActions.Disable();

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AttackButtonPerformed?.Invoke();
        }
    }

    private void OnHorizontalMovement(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            var direction = context.ReadValue<float>();
            VerticalMoveButtonUsed?.Invoke(direction);
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        _buttonJumpValue = context.ReadValue<Vector2>();
        JumpButtonUsed?.Invoke(_buttonJumpValue);
    }
}
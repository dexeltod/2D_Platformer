using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputSystemReader : MonoBehaviour
{
    [SerializeField] private PlayerMoveController _playerMoveContoller;
    [SerializeField] private PlayerAttack _playerAttack;

    private float _buttonAttackValue;
    private float _buttonJumpValue;
    private bool _buttonUseValue;
    private InputSystem _inputActions;
    private Vector2 _buttonMoveValue;

    public UnityAction ButtonUse;

    public bool ButtonUseValue => _buttonUseValue;
    public float ButtonAttackValue => _buttonAttackValue;
    public float ButtonMoveValue => _buttonMoveValue.x;
    public float ButtonJumpValue => _buttonJumpValue;


    private void Update()
    {
        Debug.Log(ButtonUse.GetInvocationList().Length);
    }

    private void Awake()
    {
        _inputActions = new InputSystem();

        _inputActions.Player.Move.performed += ctx => OnHorizontalMovement(ctx);
        _inputActions.Player.Jump.performed += ctx => OnJump(ctx);
        _inputActions.Player.Attack.performed += ctx => OnAttack(ctx);
        _inputActions.Player.Use.started += ctx => OnUse(ctx);
    }

    private void OnEnable() => _inputActions.Enable();

    private void OnDisable() => _inputActions.Disable();


    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ButtonUse?.Invoke();
            _buttonUseValue = context.ReadValueAsButton();
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _buttonAttackValue = context.ReadValue<float>();
            _playerAttack.SetButtonValue();
        }
    }

    private void OnHorizontalMovement(InputAction.CallbackContext context)
    {
        _buttonMoveValue = context.ReadValue<Vector2>();
        _playerMoveContoller.SetMoveHorizontalDirection();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        _buttonJumpValue = context.ReadValue<float>();
        _playerMoveContoller.SetJumpDir();
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemReader : MonoBehaviour
{
    [SerializeField] private PlayerMoveController _playerMoveContoller;
    [SerializeField] private PlayerAttack _playerAttack;

    private float _buttonAttackValue;
    private float _buttonUseValue;
    private InputSystem _inputActions;
    private Vector2 _buttonMoveValue;

    public float ButtonUseValue => _buttonUseValue;
    public float ButtonAttackValue => _buttonAttackValue;
    public float ButtonMoveValue => _buttonMoveValue.x;

    private void Awake()
    {
        _inputActions = new InputSystem();        

        _inputActions.Player.Move.performed += OnHorizontalMovement;
        _inputActions.Player.Move.canceled += OnHorizontalMovement;

        _inputActions.Player.Jump.started += OnJump;        
        _inputActions.Player.Jump.canceled += OnJump;

        _inputActions.Player.Attack.started += OnAttack;
        _inputActions.Player.Attack.canceled += OnAttack;

        _inputActions.Player.Use.started += OnUse;
        _inputActions.Player.Use.canceled += OnUse;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _buttonUseValue = context.ReadValue<float>();
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _buttonAttackValue = context.ReadValue<float>();
            _playerAttack.SetButtonValue(_buttonAttackValue);
        }        
    }

    private void OnHorizontalMovement(InputAction.CallbackContext context)
    {
        _buttonMoveValue = context.ReadValue<Vector2>();
        _playerMoveContoller.SetMoveHorizontalDirection(_buttonMoveValue);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
       float dirY = context.ReadValue<float>();
        _playerMoveContoller.SetJumpDir(dirY);
    }
}

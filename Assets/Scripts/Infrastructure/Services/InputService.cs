using System;
using Infrastructure.Services.Interfaces;
using UnityEngine.InputSystem;

namespace Infrastructure.Services
{
    public class InputService : IInputService
    {
        public event Action<float> VerticalButtonUsed;
        public event Action VerticalButtonCanceled;
        public event Action AttackButtonUsed;
        public event Action InteractButtonUsed;
        public event Action JumpButtonUsed;
        public event Action JumpButtonCanceled;

        private readonly InputSystem _inputSystem;

        public InputService()
        {
            _inputSystem = new InputSystem();
            EnableInputs();
        }

        ~InputService()
        {
	        DisableInputs();
        }
        
        public void EnableInputs()
        {
            _inputSystem.Enable();
            _inputSystem.Player.Move.performed += OnHorizontalMovement;
            _inputSystem.Player.Move.canceled += OnHorizontalMovement;
            _inputSystem.Player.Jump.started += OnJump;
            _inputSystem.Player.Jump.canceled += OnJump;
            _inputSystem.Player.Attack.started += OnAttack;
            _inputSystem.Player.Use.performed += OnUse;
        }

        public void DisableInputs()
        {
            _inputSystem.Disable();
            _inputSystem.Player.Move.performed -= OnHorizontalMovement;
            _inputSystem.Player.Move.canceled -= OnHorizontalMovement;
            _inputSystem.Player.Jump.started -= OnJump;
            _inputSystem.Player.Jump.canceled -= OnJump;
            _inputSystem.Player.Attack.started -= OnAttack;
            _inputSystem.Player.Use.performed -= OnUse;
        }

        private void OnUse(InputAction.CallbackContext context)
        {
            if (context.performed)
                InteractButtonUsed?.Invoke();
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            if (context.started)
                AttackButtonUsed?.Invoke();
        }

        private void OnHorizontalMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<float>();

            if (context.performed)
            {
                VerticalButtonUsed?.Invoke(direction);
            }
            else if (context.canceled)
            {
                VerticalButtonCanceled?.Invoke();
            }
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
                JumpButtonUsed?.Invoke();

            if (context.canceled)
                JumpButtonCanceled?.Invoke();
        }
    }
}
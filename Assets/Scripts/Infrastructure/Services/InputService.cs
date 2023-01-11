using System;
using UnityEngine;
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

		private readonly InputSystem _inputActions;

		public InputService()
		{
			_inputActions = new InputSystem();
			EnableInputs();
		}

		public void EnableInputs()
		{
			_inputActions.Enable();
			_inputActions.Player.Move.performed += OnHorizontalMovement;
			_inputActions.Player.Move.canceled += OnHorizontalMovement;
			_inputActions.Player.Jump.performed += OnJump;
			_inputActions.Player.Jump.canceled += OnJump;
			_inputActions.Player.Attack.performed += OnAttack;
			_inputActions.Player.Use.started += OnUse;
		}

		public void DisableInputs()
		{
			_inputActions.Disable();
			_inputActions.Player.Move.performed -= OnHorizontalMovement;
			_inputActions.Player.Move.canceled -= OnHorizontalMovement;
			_inputActions.Player.Jump.performed -= OnJump;
			_inputActions.Player.Jump.canceled -= OnJump;
			_inputActions.Player.Attack.performed -= OnAttack;
			_inputActions.Player.Use.started -= OnUse;
		}

		private void OnUse(InputAction.CallbackContext context)
		{
			if (context.started)
				InteractButtonUsed?.Invoke();
		}

		private void OnAttack(InputAction.CallbackContext context)
		{
			if (context.performed)
				AttackButtonUsed?.Invoke();
		}

		private void OnHorizontalMovement(InputAction.CallbackContext context)
		{
			if (context.performed)
			{
				var direction = context.ReadValue<float>();
				VerticalButtonUsed?.Invoke(direction);
			}

			if (context.canceled)
				VerticalButtonCanceled?.Invoke();
		}

		private void OnJump(InputAction.CallbackContext context)
		{
			if (context.performed)
				JumpButtonUsed?.Invoke();

			if (context.canceled)
				JumpButtonCanceled?.Invoke();
		}

		
	}
}
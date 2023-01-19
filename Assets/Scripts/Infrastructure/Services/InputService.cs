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

		private readonly InputSystem _inputSystem;

		public InputService()
		{
			_inputSystem = new InputSystem();
			EnableInputs();
		}

		public void EnableInputs()
		{
			_inputSystem.Enable();
			_inputSystem.Player.Move.performed += OnHorizontalMovement;
			_inputSystem.Player.Move.canceled += OnHorizontalMovement;
			_inputSystem.Player.Jump.performed += OnJump;
			_inputSystem.Player.Jump.canceled += OnJump;
			_inputSystem.Player.Attack.performed += OnAttack;
			_inputSystem.Player.Use.started += OnUse;
		}

		public void DisableInputs()
		{
			_inputSystem.Disable();
			_inputSystem.Player.Move.performed -= OnHorizontalMovement;
			_inputSystem.Player.Move.canceled -= OnHorizontalMovement;
			_inputSystem.Player.Jump.performed -= OnJump;
			_inputSystem.Player.Jump.canceled -= OnJump;
			_inputSystem.Player.Attack.performed -= OnAttack;
			_inputSystem.Player.Use.started -= OnUse;
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
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Infrastructure.Services
{
	public class InputService : IInputService
	{
		private readonly InputSystem _inputActions;
		
		public event UnityAction<float> VerticalButtonUsed;
		public event UnityAction VerticalButtonCanceled;
		public event UnityAction AttackButtonUsed;
		public event UnityAction InteractButtonUsed;
		public event UnityAction JumpButtonUsed;
		public event UnityAction JumpButtonCanceled;

		public InputService()
		{
			_inputActions = new InputSystem();
			EnableInputs();
		}

		~InputService() => 
			DisableInputs();

		public void EnableInputs()
		{
			_inputActions.Enable();
			_inputActions.Player.Move.started += OnHorizontalMovement;
			_inputActions.Player.Move.canceled += OnHorizontalMovement;
			_inputActions.Player.Jump.performed += OnJump;
			_inputActions.Player.Jump.canceled += OnJump;
			_inputActions.Player.Attack.performed += OnAttack;
			_inputActions.Player.Use.started += OnInteract;
		}

		public void DisableInputs()
		{
			_inputActions.Disable();
			_inputActions.Player.Move.started -= OnHorizontalMovement;
			_inputActions.Player.Move.canceled -= OnHorizontalMovement;
			_inputActions.Player.Jump.performed -= OnJump;
			_inputActions.Player.Jump.canceled -= OnJump;
			_inputActions.Player.Attack.performed -= OnAttack;
			_inputActions.Player.Use.started -= OnInteract;
		}

		private void OnInteract(InputAction.CallbackContext context)
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
			if (context.started)
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